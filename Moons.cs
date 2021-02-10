using UnityEngine;
using UnityEngine.UI;

public class Moons : MonoBehaviour
{
    private int moons;
    Text moonsText;

    void Start()
    {
        GetMoonsAmount();
        moonsText = GetComponent<Text>();
        UpdateDisplay();
    }


    private void GetMoonsAmount()
    {
        moons = PlayerPrefs.GetInt("Moons", 1);
    }

    private void UpdateDisplay()
    {
        moonsText.text = moons.ToString();
    }

    public void ShopCanvasClosed()
    {
        GetMoonsAmount();
        UpdateDisplay();
    }
}
