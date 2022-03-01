using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking.Types;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementAI : MonoBehaviour
{
    [Header("Chase Stats")]
    public float ChaseRad;

    [Header("SlamShock Stats")]
    public float specialCD;
    public int ChanceSpecial;
    public float[] gravityArr = new float[3];
    public float[] jumpheightArr = new float[3];
    public float JumpRange;
    public float SlamShockRad;
    public float SlamShockDamage;
    public float KBXZVelo;
    public float KBYVelo;
    public string JumpSound;
    public string LandSound;

    [Header("Surrounding Check")]
    public float CheckRad;
    public LayerMask groundMask;
    public float CheckDelay;

    private GameObject target;
    private NavMeshAgent agent;
    private CharacterController CController;
    private float specialCDtimer = 0;

    public bool jumping = false;
    private float gravity;
    private float jumpheight;
    private float YSpeed;
    private Vector3 XZVelo;
    private float CheckDelaytimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CController = GetComponent<CharacterController>();
        //target = FindObjectOfType<Camera>().transform.parent.transform; would work but what if there are no player in world
        target = PlayerSingleton.instance.player;
    }

    private void Update()
    {
        Vector3 displacement = (target.transform.position - transform.position);

        if (jumping == false)
        {
            if (displacement.magnitude <= ChaseRad)
            {
                agent.SetDestination(target.transform.position);
                if (specialCDtimer > 0)
                {
                    specialCDtimer -= Time.deltaTime;
                }
                else
                {
                    int Chance = UnityEngine.Random.Range(0, 101);
                    if (Chance < ChanceSpecial)
                    {
                        FindObjectOfType<AudioManager>().PlaySound(JumpSound);
                        gravity = gravityArr[UnityEngine.Random.Range(0, gravityArr.Length)];
                        jumpheight = jumpheightArr[UnityEngine.Random.Range(0, gravityArr.Length)];
                        YSpeed = Mathf.Sqrt(2 * gravity * jumpheight);
                        float diffY = displacement.y;
                        float timetaken = QuadraticLRoot(-gravity / 2, YSpeed, -diffY);
                        Vector3 HoriDisplacement = new Vector3(displacement.x, 0, displacement.z);
                        XZVelo =  HoriDisplacement.normalized* Mathf.Clamp(HoriDisplacement.magnitude,0,JumpRange) / timetaken;
                        jumping = true;
                        agent.enabled = false;
                        CheckDelaytimer = CheckDelay;
                    }
                    else {
                        specialCDtimer = specialCD;
                    }
                }
            }
        }
        else
        {
            YSpeed -= gravity * Time.deltaTime;
            Vector3 NetVelo = XZVelo + Vector3.up * YSpeed;
            CController.Move(NetVelo * Time.deltaTime);
            if (CheckDelaytimer > 0) {
                CheckDelaytimer -= Time.deltaTime;
            }
            if (Physics.CheckSphere(transform.position, CheckRad, groundMask) && CheckDelaytimer<=0)
            {
                FindObjectOfType<AudioManager>().PlaySound(LandSound);
                jumping = false;
                specialCDtimer = specialCD;
                YSpeed = 0;
                agent.enabled = true;
                if (displacement.magnitude <= SlamShockRad)
                {
                    target.GetComponent<HDPlayer>().Health -= SlamShockDamage;
                    target.GetComponent<HDPlayer>().UpdateHP = true;
                    target.GetComponent<PMV2>().Knockback(new Vector3(displacement.normalized.x * KBXZVelo, KBYVelo, displacement.normalized.z * KBXZVelo));
                }
            }
        }
    }

    float QuadraticLRoot(float a, float b, float c)
    {
        
        float discriminant = Math.Max(b * b - 4 * a * c,0);
        float result = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        return result;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRad);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CheckRad);
    }
}
