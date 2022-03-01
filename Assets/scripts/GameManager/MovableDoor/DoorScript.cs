using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DoorScript : MonoBehaviour
{
    public int ScoreRequired;
    public int CurrentScore = 0;
    public float checkRad;
    public LayerMask PlayerMask;
    private bool DoorUnlocked = false;
    public Vector3 VectorOffSet;

    [Header("Sounds")]
    public string DoorOpeningSound;
    public string DoorUnlockedSound;
    
    void Start()
    {
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position + VectorOffSet, checkRad, PlayerMask) && DoorUnlocked) {
            GetComponent<Animator>().enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + VectorOffSet, checkRad);
    }

    public void KillDoor() {
        Destroy(gameObject);
    }

    public void AddScore() {
        CurrentScore += 1;
        if (CurrentScore >= ScoreRequired) {
            DoorUnlocked = true;
            FindObjectOfType<AudioManager>().SetnPlay(DoorUnlockedSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            Debug.Log("Door Unlocked");
        }
    }

    public void DoorOpenSound() {
        FindObjectOfType<AudioManager>().SetnPlay(DoorOpeningSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
    }
}
