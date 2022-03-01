using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider Volume;
    public Text VolumeText;
    public Slider Sensitivity;
    public Text SensitivityText;


    public void OpenOptionPanel()
    {
        gameObject.SetActive(true);
    }
    public void CloseOptionPanel()
    {
        gameObject.SetActive(false);
    }
    public void SetVolume()
    {
        PlayerPrefs.SetFloat("Volume", Volume.value);
        VolumeText.text = Volume.value.ToString("0");
    }
    public void SetSens()
    {
        PlayerPrefs.SetFloat("Sensitivity", Sensitivity.value);
        SensitivityText.text = Sensitivity.value.ToString("0");
    }
}
