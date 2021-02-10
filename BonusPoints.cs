using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    [SerializeField] GameObject bonusPointsCanvas;

    public void UseStars() //Called via a button.
    {
        var stars = PlayerPrefs.GetInt("Stars", 0); // Get the amount of Stars available to the player.
        if (stars >= 100)
        {
            stars -= 100;
            PlayerPrefs.SetInt("Stars", stars);
            FindObjectOfType<GameManager>().IncreaseScoreBy1000();
            Time.timeScale = 1;
            bonusPointsCanvas.SetActive(false);
            FindObjectOfType<Player>().ChangePlayerState();
        }
        else
        {
            FindObjectOfType<GameManager>().LoadShopCanvas();
            Debug.Log("Shop Canvas Should have loaded");
        }
    }

    public void StartGame() //Called via a button.
    {
        Time.timeScale = 1;
        bonusPointsCanvas.SetActive(false);
        FindObjectOfType<Player>().ChangePlayerState();
    }
}