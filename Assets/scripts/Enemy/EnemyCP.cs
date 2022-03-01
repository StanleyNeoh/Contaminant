using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCP : MonoBehaviour
{
    public int WeaponisedLayer;
    public string HurtSound;
    public string DeathSound;
    public GameObject HealthPack;

    [Header("Core")]
    public float Health0;
    public float DespawnTime0;
    public GameObject ExplosionEffect;
    public float NatPopOffV0;
    public float PurposedPopOffV0;
    public float ChancePurposed0;
    public float collisionDperV0;
    public float Mass0;

    [Header("Layer 1")]
    public float Health1;
    public float DespawnTime1;
    public float NatPopOffV1;
    public float PurposedPopOffV1;
    public float ChancePurposed1;
    public float collisionDperV1;
    public float Mass1;

    [Header("Layer 2")]
    public float Health2;
    public float DespawnTime2;
    public float NatPopOffV2;
    public float PurposedPopOffV2;
    public float ChancePurposed2;
    public float collisionDperV2;
    public float Mass2;

    [HideInInspector]
    public float[] HealthData = new float[3]; //new is used to initialise all kinds of variables (calls its constructor)
    [HideInInspector]
    public float[] DespawnTimeData = new float[3];
    [HideInInspector]
    public float[] NatPopOffVData = new float[3];
    [HideInInspector]
    public float[] PurposedPopOffVData = new float[3];
    [HideInInspector]
    public float[] ChancePurposedData = new float[3];
    [HideInInspector]
    public float[] collisionDperVData = new float[3];
    [HideInInspector]
    public float[] MassData = new float[3];


    public void ChildStats() {
        HealthData[0] = Health0;
        HealthData[1] = Health1;
        HealthData[2] = Health2;
        DespawnTimeData[0] = DespawnTime0;
        DespawnTimeData[1] = DespawnTime1;
        DespawnTimeData[2] = DespawnTime2;
        NatPopOffVData[0]= NatPopOffV0;
        NatPopOffVData[1] = NatPopOffV1;
        NatPopOffVData[2] = NatPopOffV2;
        PurposedPopOffVData[0] = PurposedPopOffV0;
        PurposedPopOffVData[1] = PurposedPopOffV1;
        PurposedPopOffVData[2] = PurposedPopOffV2;
        ChancePurposedData[0] = ChancePurposed0;
        ChancePurposedData[1] = ChancePurposed1;
        ChancePurposedData[2] = ChancePurposed2;
        collisionDperVData[0]= collisionDperV0;
        collisionDperVData[1]= collisionDperV1;
        collisionDperVData[2]= collisionDperV2;
        MassData[0]= Mass0;
        MassData[1]= Mass1;
        MassData[2]= Mass2;
    }
}

