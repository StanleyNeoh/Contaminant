using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    Rigidbody rb;
    HDPlayer playerhealth;
    public Vector3 StartingVelo;
    public Vector3 StartingAngularVelo;
    public float pickupdistance;
    public float healthreceived;
    public string PickupSound;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(StartingVelo, ForceMode.VelocityChange);
        rb.AddTorque(StartingAngularVelo, ForceMode.VelocityChange);
        playerhealth = FindObjectOfType<HDPlayer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerhealth.gameObject.transform.position, transform.position) <= pickupdistance) 
        {
            playerhealth.Health += healthreceived;
            playerhealth.UpdateHP = true;
            FindObjectOfType<AudioManager>().SetnPlay(PickupSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            Destroy(gameObject);
        }
    }
}
