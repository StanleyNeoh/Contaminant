using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class PMV2: MonoBehaviour
{
    public CharacterController CController;

    [Header("PlayerStats")]
    public float Speed = 30f;
    public float StrafeAcceleration = 30f;
    public float Gravity = -20f;
    public float JumpHeight = 10.0f;

    private Vector3 XZVelo;
    private Vector3 YVelo;
    private float CosTheta;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundDistance = 0.6f; //set slightly bigger than radius of CController
    public float VertOffSet;
    public LayerMask groundMask;
    public float checkDelay;
    private float checkDelaytimer;
    private bool isGrounded;

    private Vector3 NetVelo;
    private bool KnockedBack = false;
    private Vector3 KnockedbackVelo;

    void FixedUpdate()
    {        
        float xtranslation = Input.GetAxis("Horizontal");
        float ztranslation = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundCheck.position + Vector3.up *VertOffSet , groundDistance, groundMask);

        if (isGrounded) {
            XZVelo = transform.right * xtranslation + transform.forward * ztranslation;
            if (XZVelo.magnitude > 1) {
                XZVelo = XZVelo.normalized;
            }
            XZVelo *= Speed; //new Vector3(x,y,z) will reference global coordinate
            if (YVelo.y < 0f) {
                YVelo.y = -0.1f;
            }
            if (Input.GetButtonDown("Jump")) {
                YVelo.y = Mathf.Sqrt(-2.0f * Gravity * JumpHeight);
            }
        }
        else {
            Vector3 strafe = (transform.right * xtranslation + transform.forward * ztranslation).normalized;
            CosTheta = Vector3.Dot(XZVelo.normalized, strafe );
            XZVelo += StrafeAcceleration * (1-CosTheta)* (1 - CosTheta) * strafe * Time.deltaTime;
        } //(1-cosTheta) squared is to make it more significant

        YVelo.y += Gravity * Time.deltaTime;
        if (KnockedBack)
        {
            YVelo.y = KnockedbackVelo.y;
            XZVelo = new Vector3(KnockedbackVelo.x, 0, KnockedbackVelo.z);
            KnockedBack = false;
        }

        NetVelo = XZVelo + YVelo;
        CController.Move(NetVelo * Time.deltaTime); // dy = v dt
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position + Vector3.up * VertOffSet, groundDistance);
    }

    public void Knockback(Vector3 InitialMotion) {
        KnockedbackVelo = InitialMotion;
        KnockedBack = true;
    }
}
