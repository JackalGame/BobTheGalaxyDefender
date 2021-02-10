using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopCanvas;
    private int stars;
    private int moons;
    public Text moonsText;
    public Text starsText;

    private void Start()
    {
        shopCanvas.SetActive(false);
        GetCurrencyTotal();
        UpdateDisplay();
    }

    public void LoadShopCanvas()
    {
        shopCanvas.SetActive(true);
        GetCurrencyTotal();
        UpdateDisplay();
        
    }

    public void CloseShopCanvas()
    {
        shopCanvas.SetActive(false);
        FindObjectOfType<Stars>().ShopCanvasClosed();
        FindObjectOfType<Moons>().ShopCanvasClosed();
    }

    private void UpdateDisplay()
    {
        moonsText.text = moons.ToString();
        starsText.text = stars.ToString();
    }

    private void GetCurrencyTotal()
    {
        stars = PlayerPrefs.GetInt("Stars", 0);
        moons = PlayerPrefs.GetInt("Moons", 1);
    }

    public void PurchaseStars(int totalStarsPurchased)
    {
        stars += totalStarsPurchased;
        PlayerPrefs.SetInt("Stars", stars);
        UpdateDisplay();
    }

    public void PurchaseMoons (int totalMoonsPurchased)
    {
        moons += totalMoonsPurchased;
        PlayerPrefs.SetInt("Moons", moons);
        UpdateDisplay();
    }

    public void SwapMoonForStars()
    {
        if (moons >= 1)
        {
            moons--;
            PlayerPrefs.SetInt("Moons", moons);
            stars += 500;
            PlayerPrefs.SetInt("Stars", stars);
            UpdateDisplay();
        }
        
    }

    public void ResetValues()
    {
        PlayerPrefs.SetInt("Stars", 0);
        PlayerPrefs.SetInt("Moons", 1);
        UpdateDisplay();
    }
}
