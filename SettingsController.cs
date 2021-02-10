using UnityEngine;
using UnityEngine.UI;


public class SettingsController : MonoBehaviour
{
    [SerializeField] GameObject resetHighscoreCanvas;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    

    void Start()
    {
        resetHighscoreCanvas.SetActive(false);
        musicVolumeSlider.value = PlayerPrefsController.GetMasterMUSICVolume();
        sfxVolumeSlider.value = PlayerPrefsController.GetMasterSFXVolume();
    }

    void Update()
    {
        var musicPlayer = FindObjectOfType<BackgroundMusic>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(musicVolumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found... start from logo scene");
        }
    }


    public void ResetButton()
    {
        resetHighscoreCanvas.SetActive(true);
    }

    public void DoNotResetHighscore()
    {
        resetHighscoreCanvas.SetActive(false);
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        resetHighscoreCanvas.SetActive(false);
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterMusicVolume(musicVolumeSlider.value);
        PlayerPrefsController.SetMasterSFXVolume(sfxVolumeSlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
}
