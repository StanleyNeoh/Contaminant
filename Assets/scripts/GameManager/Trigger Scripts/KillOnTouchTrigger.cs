using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouchTrigger : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = PlayerSingleton.instance.player;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("die");
            other.gameObject.GetComponent<HDPlayer>().Health = 0;
            other.gameObject.GetComponent<HDPlayer>().UpdateHP = true;
        }
        if (other.gameObject.GetComponent<UniversalDestructible>().layer == 0)
        {
            if (gameObject.name == "Kill On Touch")
            {
                FindObjectOfType<DoorScript>().AddScore();
                Debug.Log("score added");
            }
        }
    }
}
