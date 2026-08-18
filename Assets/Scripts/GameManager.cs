using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject gameStartUI;

    [SerializeField]
    private GameObject shopUI;

    public PlayerInput playerInput;

    private bool gameStarted = false;
    private bool gameOver = false;

    public int StartFrame { get; private set; }

    public bool IsGameStarted => gameStarted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            playerInput = FindAnyObjectByType<PlayerInput>();
        }

        Time.timeScale = 0f;
    }

    void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (gameStartUI != null)
        {
            gameStartUI.SetActive(true);
        }

        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void Update()
    {
        if (!gameStarted && !gameOver)
        {
            if(playerInput == null)
            {
                Debug.LogWarning("PlayerInput is Null! Ensure PlayerInput component exsits and assigned");
                return;
            }

            var jumpAction = playerInput?.actions["jump"];

            if (jumpAction == null)
            {
               Debug.LogWarning("Jump action not found Check action name in the Input ACtions");
               return; 
            }

            if (jumpAction.WasPressedThisFrame())
            {
                Debug.Log("Jump action detected"); 
                if (!IsPointedOverUI())
                {
                    StartGame(); 
                }
                else
                {
                    Debug.Log("Pointer over UI. skipping Start.");
                }
            }
        }
    }

    private bool IsPointedOverUI()
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        return EventSystem.current.IsPointerOverGameObject();
    }

    public void StartGame()
    {
        if (gameStarted)
        {
            return;
        }

        gameStarted = true;
        StartFrame = Time.frameCount;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        if (gameStartUI != null)
        {
            gameStartUI.SetActive(false);
        }

        Time.timeScale = 1f;

        Debug.Log("Game started");
        SoundManager.Instance.PlayGameplayMusic(); 

    }

    public void OpenShop()
    {
        if (shopUI != null)
        {
            shopUI.SetActive(true);

            SkinShopUI shop = shopUI.GetComponent<SkinShopUI>();
            if (shop != null)
            {
                shop.RefreshShop();
            }
            gameStartUI.SetActive(false);
        }

        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        if (shopUI != null)
        {
            shopUI.SetActive(false);
            gameStartUI.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        if (gameOver)
        {
            return;
        }

        gameOver = true;

        SoundManager.Instance.PlayGameOver();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            gameOverUI = GameObject.Find("GameOverUI");
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
            else
            {
                Debug.LogError("GameOverUI not found in the scene");
            }
        }

        Time.timeScale = 0f;

        if (Score.Instance != null)
        {
            CurrencyManager.Instance.AddCurrency(Score.Instance.CurrentScore);
            Debug.Log($"Score this round {Score.Instance.CurrentScore} added to currency");
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
