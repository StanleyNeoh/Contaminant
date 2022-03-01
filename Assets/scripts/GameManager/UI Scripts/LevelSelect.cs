using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Slider LevelSelector;
    public int LevelToLoad = 1;
    public Text Number;
    public GameObject[] descriptions = new GameObject[10];



    public void OpenLevelSelectPanel()
    {
        gameObject.SetActive(true);
    }
    public void CloseLevelSelectPanel()
    {
        gameObject.SetActive(false);
    }

    public void UpdateLevel() {
        LevelToLoad = Convert.ToInt32(LevelSelector.value);
        Number.text = LevelSelector.value.ToString("0");
        for(int i = 0; i<descriptions.Length; i++) 
        {
            if (i + 1 == LevelToLoad)
            {
                descriptions[i].SetActive(true);
            }
            else {
                descriptions[i].SetActive(false);
            }
        }

    }

    public void LoadLevel() {
        SceneManager.LoadScene(LevelToLoad);
    }
}
