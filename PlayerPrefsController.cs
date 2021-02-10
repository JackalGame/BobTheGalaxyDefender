using UnityEngine;


public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_MUSIC_VOLUME_KEY = "master music volume";
    const string MASTER_SFX_VOLUME_KEY = "master SFX volume";

    const float MIN_MUSICVOLUME = 0f;
    const float MAX_MUSICVOLUME = 1f;
    const float MIN_SFXVOLUME = 0f;
    const float MAX_SFXVOLUME = 1f;

    public static void SetMasterMusicVolume(float volume)
    {
        if(volume >= MIN_MUSICVOLUME && volume <= MAX_MUSICVOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master Music Volume is out of range");
        }
    }

    public static float GetMasterMUSICVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_MUSIC_VOLUME_KEY, 0.5f);
    }

    public static void SetMasterSFXVolume(float volume)
    {
        if(volume >= MIN_SFXVOLUME && volume <= MAX_SFXVOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_SFX_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master SFX Volume is out of range");
        }
    }

    public static float GetMasterSFXVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_SFX_VOLUME_KEY, 0.8f);
    }
}
