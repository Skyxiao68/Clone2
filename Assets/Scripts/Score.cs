using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private int score ;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
    }

    private void Start()
    {

        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore(); 
    }

    private void UpdateHighScore()
    {
        
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score); 
            highScoreText.text = "High Score: " + score.ToString(); 
        }
    }

    public void AddScore()
    {
        score++; 
        scoreText.text = "Score: " + score.ToString();
        UpdateHighScore(); 
    }



}
