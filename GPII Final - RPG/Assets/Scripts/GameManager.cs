using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;

    public TextMeshProUGUI scoreText;
    public bool hasTriedScoreText = false; 

    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTriedScoreText)
        {
            hasTriedScoreText = true;
            if (scoreText == null)
            {
                scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<TextMeshProUGUI>();
            }
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
        if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == 0)
        {
            score = 0;
            hasTriedScoreText = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == 2)
        {
            score = 0;
            hasTriedScoreText = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }

        if (score >= 4 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
