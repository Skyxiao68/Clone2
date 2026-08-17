using UnityEngine;

public class PlayerVisual : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float frameRate = 10f; 

    private Sprite[] currentFrames;
    private int currentFrameIndex = 0;

    private float timer = 0f; 

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        ApplySelectedSkin();
    }

    void Update()
    {
        if (currentFrames == null || currentFrames.Length == 0 )
        {
            return;
        }

        timer += Time.deltaTime * frameRate;

        if (timer >= 1f)
        {
            timer -= 1f; 
            currentFrameIndex = (currentFrameIndex + 1) % currentFrames.Length;
            spriteRenderer.sprite = currentFrames[currentFrameIndex];  
        }
    }

    public void ApplySelectedSkin()
    {
        

        if (SkinManager.Instance == null || SkinManager.Instance.SelectedSkin == null)
        {
            Debug.LogWarning("SkinManager 或 SelectedSkin not ready using default skin.");
            return;
        }

        Skin selected = SkinManager.Instance.SelectedSkin; 
        currentFrames = selected.animationFrames;

        if (currentFrames != null && currentFrames.Length > 0)
        {
            currentFrameIndex = 0; 
            spriteRenderer.sprite = currentFrames[0];
        }
        else
        {
            Debug.LogWarning($"Selected skin has no animation frames, check resource/skins/{selected.id} folder");
        }
    

    }
}