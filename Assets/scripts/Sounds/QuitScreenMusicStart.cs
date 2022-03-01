using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScreenMusicStart : MonoBehaviour
{
    public string QuitScreenMusic;
    public string thankYou;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<AudioManager>().SetnPlay(QuitScreenMusic, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
        FindObjectOfType<AudioManager>().SetnPlay(thankYou, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
    }
}
