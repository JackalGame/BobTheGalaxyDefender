using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    [SerializeField] int timeToWait = 2;
    int currentSceneIndex;
    
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadLoadingScene()
    {
        SceneManager.LoadScene("Loading Scene");
    }

    public void LoadGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game Scene");
        FindObjectOfType<GameManager>().SetCanvasState();
    }

    public void ReloadGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game Scene");
        FindObjectOfType<GameManager>().DestroyGameManager();
    }

    public void UseMoon()
    {
        var moons = PlayerPrefs.GetInt("Moons", 1);
        if(moons >= 1)
        {
            moons--;
            PlayerPrefs.SetInt("Moons", moons);
            Time.timeScale = 1;
            SceneManager.LoadScene("Game Scene");
            FindObjectOfType<GameManager>().SetCanvasState();
        }
        else
        {
            FindObjectOfType<GameManager>().LoadShopCanvas();
        }
    }

    public void FinishGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu Scene");
        FindObjectOfType<GameManager>().DestroyGameManager();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadAlienDataScene()
    {
        SceneManager.LoadScene("Alien Data Scene");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings Scene");
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits Scene");
    }

    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("Instructions Scene");
    }

    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/jackal_game/");
    }

    public void LoadAlienInfo(string alienName)
    {
        SceneManager.LoadScene(alienName);
    }

}

 