using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    private int stars;
    Text starsText;

    void Start()
    {
        GetMoonsAmount();
        starsText = GetComponent<Text>();
        UpdateDisplay();
    }


    private void GetMoonsAmount()
    {
        stars = PlayerPrefs.GetInt("Stars", 0);
    }

    public void UpdateDisplay()
    {
        starsText.text = stars.ToString();
    }

    public void ShopCanvasClosed()
    {
        GetMoonsAmount();
        UpdateDisplay();
    }

    public void AddFiveStars()
    {
        int totalStars = PlayerPrefs.GetInt("Stars", 0);
        totalStars += 5;
        PlayerPrefs.SetInt("Stars", totalStars);
        Debug.Log("Stars Added");
    }


}
