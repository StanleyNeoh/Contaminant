using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Gunfire : MonoBehaviour
{
    
    [Header("Reference")]
    public GameObject bullet;
    public Transform gunPos;
    public string GunFireSound;

    [Header("Gun Stats")]
    public float TimeBetween;
    public int count;
    public float spreadSpawn;
    public float ForwardSpawn;
    public bool HasLaserSight;

    private float timer = 0;
    private Vector3 SpawnVect;
    private LineRenderer LR;

    private void Start()
    {
        if (HasLaserSight) {
            LR = GetComponentInChildren<LineRenderer>();
            LR.useWorldSpace = false; //setposiion is based on worldspace or parentspace. depends on this.
        }

    }

    void Update()
    {
        if (HasLaserSight)
        {
            Vector3 spawnPos = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(spawnPos, Vector3.forward , out hit)) // wrt global
            {
                if (hit.collider)
                {
                    LR.SetPosition(1, new Vector3(0,0,hit.distance));
                }
            }
            else
            {
                LR.SetPosition(1, new Vector3(0, 0, 1000));
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetKey("mouse 0") && timer <= 0 && gameObject.GetComponentInParent<HDPlayer>().Health > 0)
        {
            FindObjectOfType<AudioManager>().SetnPlay(GunFireSound, PlayerPrefs.GetFloat("Volume", 100) / 100, 1);
            if (count > 1)
            {
                for (int i = 1; i <= count; i++)
                {
                    SpawnVect = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * spreadSpawn;
                    Instantiate(bullet, gunPos.position + transform.forward * 3 + SpawnVect, gunPos.rotation);
                }
            }
            else
            {
                Instantiate(bullet, gunPos.position + transform.forward * ForwardSpawn, gunPos.rotation);
            }

            timer = TimeBetween;
        }
    }
}
