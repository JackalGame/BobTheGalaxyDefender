using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGameScene : MonoBehaviour
{
    void Start()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);
    }
}
