using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.EventSystems; 
using UnityEngine.InputSystem.Layouts; 

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; 

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameStartUI; 
    [SerializeField] private GameObject shopUI; 

    public PlayerInput playerInput; 



    private bool gameStarted = false; 
    private bool gameOver = false; 

    public bool IsGameStarted => gameStarted; 


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
   private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 

            
        }
        else
        {
            Destroy(gameObject); 
            return; 
        }

        playerInput = GetComponent<PlayerInput>(); 
        if(playerInput == null)
        {
            playerInput = FindAnyObjectByType<PlayerInput>(); 
        }



        Time.timeScale = 0f;

    }


    private void OnEnable()
    {
        if (playerInput != null)
        {
           playerInput.actions["jump"].performed += OnJumpPerformed; 

        }
    }

    private void OnDisable()
    {
        if (playerInput != null)
        {
           playerInput.actions["jump"].performed -= OnJumpPerformed; 

        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (!gameStarted && !gameOver)
        {
            if (!IsPointedOverUI())
            {
                StartGame(); 
            }

        }
    }

    private bool IsPointedOverUI()
    {
        if (EventSystem.current == null )
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

        if(gameStartUI != null)
        {
            gameStartUI.SetActive(false); 
        }   

        Time.timeScale = 1f;

        SoundManager.Instance.PlayGameplayMusic(); 

        Debug.Log("Game started"); 
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); 
        
    }
}
