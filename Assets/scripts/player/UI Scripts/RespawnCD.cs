using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnCD : MonoBehaviour
{
    public float RespawnTime;
    private Text timeleft;
    private float timer;
    private void OnEnable() //can use start() and activate gameObject instead
    {
        timer = RespawnTime;
        timeleft = gameObject.GetComponent<Text>();
        timeleft.text = RespawnTime.ToString("0");
    }
    private void Update()
    {
        timeleft.text = timer.ToString("0");
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        if (timer <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
