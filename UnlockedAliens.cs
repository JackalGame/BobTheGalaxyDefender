using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedAliens : MonoBehaviour
{
    [SerializeField] string alienName;
    int alienUnlocked;

    public Button button;

    private void Awake()
    {
        alienUnlocked = PlayerPrefs.GetInt(alienName, 0);

        if (alienUnlocked <= 0)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
}
