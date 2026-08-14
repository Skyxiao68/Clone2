using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; 

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameStartUI; 
    [SerializeField] private GameObject shopUI; 

    private bool gameStarted = false; 
    private bool gamePaused = false; 


    void Start()
    {
        

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        
    }
   private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 

            
        }

        

        Time.timeScale = 1f;

    }

    public void GameOver()
    {
        if (gameOverUI == null)
        {
            gameOverUI = GameObject.Find("GameOverUI"); 
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); 
        }
        else
        {
            Debug.LogError("GameOverUI not found in the scene"); 
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
