using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    private int score = 0; 
    public int highScore;
    public static int lastScore = 0;

    public Text highScoreText;
    public Text lastScoreText;  

    // Singleton örneği
    public static ScoreManager Instance { get; private set; }


    private void Update()
    {
        if(score>highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("high_score", highScore);
            
        }
    }

    void Awake()
    {
        // Singleton örneğini oluşturun
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        
        InvokeRepeating("IncreaseScoreOverTime", 1.0f, 1.0f);
        UpdateScoreText(); // Başlangıçta skoru güncelle
        

        highScore = PlayerPrefs.GetInt("high_score", 0);
        lastScore = 0;

        highScoreText.text = "High Score: "  + highScore.ToString();
        lastScoreText.text = "Last Score: " + lastScore.ToString();




    }

    void IncreaseScoreOverTime()
    {
        score += 1;
        UpdateScoreText();
    }

    internal int GetScore()
    {
        throw new NotImplementedException();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    IEnumerator Score()
    {
        while(true)
        {
            lastScore = score;
        }
    }

   
}
