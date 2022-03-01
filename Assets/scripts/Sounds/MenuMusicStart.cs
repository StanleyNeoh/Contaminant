using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicStart : MonoBehaviour
{
    public string MenuMusic;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<AudioManager>().SetnPlay(MenuMusic, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
    }
}
