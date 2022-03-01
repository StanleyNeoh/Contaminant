using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HDPlayer : MonoBehaviour
{ 
    [Header("For Player")]
    public float Health;
    public float MinDamageV;
    public string HurtSound;
    public GameObject DeathScreen;
    public GameObject HealthbarObj;
    //public Healthbar SetHealthbar;

    [Header("EnemyDetect")]
    public LayerMask EnemyMask;
    public float HeadOffSet;
    public float SearchRad;
    public float EnemyDamage;
    public float AttackCD;
    private float AttackCDtimer;
    
    [HideInInspector]
    public float maxHealth;
    [HideInInspector]
    public bool UpdateHP = false;

    void Start()
    {
        maxHealth = Health;
        HealthbarObj.GetComponent<Healthbar>().SetHealth(Health);
        HealthbarObj.GetComponent<Healthbar>().SetMaxHealth(Health);

    }

    void Update()
    {
        
        if (Health <= 0)
        {
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (UpdateHP) {
            if (HealthbarObj.GetComponent<Slider>().value > Health) {
                FindObjectOfType<AudioManager>().SetnPlay(HurtSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            }
            HealthbarObj.GetComponent<Healthbar>().SetHealth(Health);
            UpdateHP = false;
        }

        if (Physics.CheckSphere(transform.position + Vector3.up * HeadOffSet, SearchRad,EnemyMask) && AttackCDtimer <= 0)
        {
            Health -= EnemyDamage;
            UpdateHP = true;
            if (Physics.OverlapSphere(transform.position + Vector3.up * HeadOffSet, SearchRad, EnemyMask)[0].GetComponentInParent<Jump2>().jumping == 0) {
                AttackCDtimer = AttackCD;
            }
        }
        else if (AttackCDtimer > 0 ){
            AttackCDtimer -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<UniversalDestructible>() != null) { 
            UniversalDestructible HDdata = collision.collider.gameObject.GetComponent<UniversalDestructible>();
            Vector3 ProjMotion = collision.collider.gameObject.GetComponent<Rigidbody>().velocity;
            if (ProjMotion.magnitude >= MinDamageV)
            {
                Health -= HDdata.collisionDperV * ProjMotion.magnitude;
                UpdateHP = true;
                Debug.Log("Ouch");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * HeadOffSet, SearchRad);
    }
}
