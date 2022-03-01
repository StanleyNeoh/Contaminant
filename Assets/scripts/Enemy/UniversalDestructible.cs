using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UniversalDestructible : MonoBehaviour
{
    [Header("Parent Data Access")]
    public GameObject Parent;
    public int layer;

    [Header("BlockData")]   
    public float DespawnTime;
    public float NatPopOffV;
    public float PurposedPopOffV;
    public float ChancePurposed;
    public float collisionDperV;
    public string HurtSound;
    public string DeathSound;
    public float Health;

    private GameObject player;
    private Rigidbody myself;
    private float Timer;
    private bool notpopped = true;
    private bool toCD = false;

    void Start()
    {
        player = PlayerSingleton.instance.player;
        myself = GetComponent<Rigidbody>();

        if (Parent != gameObject) {
            Parent.GetComponent<EnemyCP>().ChildStats(); //run function from parent script , could initialsise script directly (public EnemyCP stats)
            Health = Parent.GetComponent<EnemyCP>().HealthData[layer];
            DespawnTime = Parent.GetComponent<EnemyCP>().DespawnTimeData[layer];
            NatPopOffV = Parent.GetComponent<EnemyCP>().NatPopOffVData[layer];
            PurposedPopOffV = Parent.GetComponent<EnemyCP>().PurposedPopOffVData[layer];
            ChancePurposed = Parent.GetComponent<EnemyCP>().ChancePurposedData[layer];
            collisionDperV = Parent.GetComponent<EnemyCP>().collisionDperVData[layer];
            myself.mass = Parent.GetComponent<EnemyCP>().MassData[layer];
            HurtSound = Parent.GetComponent<EnemyCP>().HurtSound;
            DeathSound = Parent.GetComponent<EnemyCP>().DeathSound;
        }

        if (Health > 0) {
            myself.isKinematic = true;
        }
        Timer = DespawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0) {
            myself.isKinematic = false;
            myself.useGravity = true;
            toCD = true;
            if (Parent != null) {
                gameObject.layer = Parent.GetComponent<EnemyCP>().WeaponisedLayer;
            }
            if (notpopped)
            {
                int RNG = UnityEngine.Random.Range(0,101);
                if (RNG < ChancePurposed && Parent != gameObject)
                {
                    Vector3 direction = (FindObjectOfType<Camera>().transform.position - transform.position).normalized;
                    myself.AddForce(direction * PurposedPopOffV, ForceMode.VelocityChange);
                }
                else {
                    myself.AddExplosionForce(NatPopOffV, Parent.transform.position, 100f, 0f, ForceMode.VelocityChange);
                }  
                notpopped = false;
            }
        }

        if (toCD && Timer>0) {
            Timer -= Time.deltaTime;
            if (layer != 0 && Parent != null)
            {
                gameObject.transform.parent = null;
            }
        }

        if (Timer <= 0) {
            if (layer == 0 && Parent != gameObject)
            {
                GameObject.Find("Movable Door").GetComponent<DoorScript>().AddScore();
                Instantiate(Parent.GetComponent<EnemyCP>().ExplosionEffect, transform.position, transform.rotation);
                Instantiate(Parent.GetComponent<EnemyCP>().HealthPack, transform.position,Quaternion.Euler(Vector3.right));
                FindObjectOfType<AudioManager>().PlaySound(DeathSound);
                Destroy(Parent);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bulletphy colliderdata = collision.collider.gameObject.GetComponent<bulletphy>();
        if (colliderdata != null)
        {
            Health -= colliderdata.bulletDamage;
            FindObjectOfType<AudioManager>().SetnPlay(HurtSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            if (Parent.GetComponent<Jump2>().Agro == false) {
                Parent.GetComponent<Jump2>().Agro = true;
            }
            if (colliderdata.Vampiric) {
                int chance = UnityEngine.Random.Range(0, 101);
                if (chance < colliderdata.ChanceHeal) {
                    player.GetComponent<HDPlayer>().Health += colliderdata.HPperShot;
                    player.GetComponent<HDPlayer>().UpdateHP = true;
                } 
            }
        }
    }
}
