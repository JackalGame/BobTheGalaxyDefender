using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] float pointIncreasedPerSecond = 1f;

    [Header("Canvas'")]
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject firstGameOverCanvas;
    [SerializeField] GameObject secondGameOverCanvas;
    [SerializeField] GameObject thirdGameOverCanvas;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject bonusPointsCanvas;

    [Header ("Canvas Scores")]
    [SerializeField] Text firstHighscoreText;
    [SerializeField] Text firstPlayerScoreText;
    [SerializeField] Text secondHighscoreText;
    [SerializeField] Text secondPlayerScoreText;
    [SerializeField] Text thirdHighscoreText;
    [SerializeField] Text thirdPlayerScoreText;


    private int playerDeaths = 0;
    private float scoreAmount;
    private float bonusScoreAmount;
    public int totalScoreAmount;


    private void Awake()
    {
        //Ensures that only one GameManager persists between scenes.
        int numGameSessions = FindObjectsOfType<GameManager>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
        GetHighScore();
        firstHighscoreText.text = GetHighScore().ToString();
        secondHighscoreText.text = GetHighScore().ToString();
        thirdHighscoreText.text = GetHighScore().ToString();
    }

    void Start()
    {
        firstGameOverCanvas.SetActive(false);
        secondGameOverCanvas.SetActive(false);
        thirdGameOverCanvas.SetActive(false);
        shopCanvas.SetActive(false);

        if (playerDeaths == 0)
        {
            Time.timeScale = 0;
            bonusPointsCanvas.SetActive(true);
            FindObjectOfType<Player>().ChangePlayerState();
        }
        else
        {
            bonusPointsCanvas.SetActive(false);
        }

    }

    void Update()
    {
        IncreaseScore();
    }


    public void IncreaseScore()
    {
        scoreText.text = totalScoreAmount.ToString();
        firstPlayerScoreText.text = totalScoreAmount.ToString();
        secondPlayerScoreText.text = totalScoreAmount.ToString();
        thirdPlayerScoreText.text = totalScoreAmount.ToString();
        scoreAmount += pointIncreasedPerSecond * Time.deltaTime;
        totalScoreAmount = (int) scoreAmount + (int) bonusScoreAmount;

        if(totalScoreAmount > GetHighScore())
        {
            PlayerPrefs.SetFloat("Highscore", (int) totalScoreAmount);
            firstHighscoreText.text = totalScoreAmount.ToString();
            secondHighscoreText.text = totalScoreAmount.ToString();
            thirdHighscoreText.text = totalScoreAmount.ToString();
        }
    }

    public int GetHighScore()
    {
        float highscore = PlayerPrefs.GetFloat("Highscore");
        return (int) (float) highscore;
    }

    public void HandleLoseCondition()
    {
        mainCanvas.SetActive(false);
        if (playerDeaths == 0)
        {
            firstGameOverCanvas.SetActive(true);
            playerDeaths++;
        }
        else if(playerDeaths == 1)
        {
            secondGameOverCanvas.SetActive(true);
            playerDeaths++;
        }
        else   
        {
            thirdGameOverCanvas.SetActive(true);
        }
        
        
        Time.timeScale = 0;
    }

    public void AddBonusScore(float bonusScore)
    {
        bonusScoreAmount += bonusScore;
    }

    public void DestroyGameManager()
    {
        Destroy(gameObject);
    }

    public void SetCanvasState()
    {
        firstGameOverCanvas.SetActive(false);
        secondGameOverCanvas.SetActive(false);
        thirdGameOverCanvas.SetActive(false);
        shopCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void LoadShopCanvas()
    {
        shopCanvas.SetActive(true);
    }

    public void IncreaseScoreBy1000()
    {
        scoreAmount += 1000;
    }
}
