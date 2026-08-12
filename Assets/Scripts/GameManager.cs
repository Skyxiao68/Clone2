using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; 

    [SerializeField] private GameObject gameOverUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        gameOverUI.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); 
    }
}
