using System.Collections;
using UnityEngine;


public class BackgroundMusic : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        SetUpSingleton();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterMUSICVolume();
    }

    //Ensures that the music player is transfers when scene is changed.
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
        
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
