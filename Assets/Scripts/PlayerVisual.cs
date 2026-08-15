using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplySelectedSkin();
    }

   
    public void ApplySelectedSkin()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (SkinManager.Instance == null || SkinManager.Instance.SelectedSkin == null)
        {
            Debug.LogWarning("SkinManager 或 SelectedSkin 未准备好，使用默认皮肤。");
            return;
        }

        string skinID = SkinManager.Instance.SelectedSkin.id;
        Sprite skinSprite = Resources.Load<Sprite>("Skins/" + skinID);

        if (skinSprite != null)
        {
            spriteRenderer.sprite = skinSprite;
        }
        else
        {
            Debug.LogWarning($"找不到皮肤 Sprite：Skins/{skinID}，保留原外观。");
        }
    }
}