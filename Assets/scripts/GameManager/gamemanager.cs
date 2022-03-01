using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
    public string InGameMusic;
    public string Contamination1;
    public string NumberSound;
    public string Contamination2;

    public string Mutated;
    public string NewWeapon;
    private void Start()
    {
        FindObjectOfType<AudioManager>().SetnPlay(InGameMusic, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
        StartCoroutine(IntroSpeech());
    }

    IEnumerator IntroSpeech() {
        FindObjectOfType<AudioManager>().SetnPlay(Contamination1, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().ClipLength(Contamination1));
        FindObjectOfType<AudioManager>().SetnPlay(NumberSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().ClipLength(NumberSound));
        FindObjectOfType<AudioManager>().SetnPlay(Contamination2, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().ClipLength(Contamination2));
        
        if (Mutated != "")
        {
            FindObjectOfType<AudioManager>().SetnPlay(Mutated, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            yield return new WaitForSeconds(FindObjectOfType<AudioManager>().ClipLength(Mutated));
        }
        if (NewWeapon != "")
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<AudioManager>().SetnPlay(NewWeapon, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
        }
    }

    public void nextscreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FirstScreen()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
