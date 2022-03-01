using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    //singleton is a pattern (a reusable solution technique) creating a whole class/script for a single global variable
    #region Singleton

    public static PlayerSingleton instance; //making it static made it into a global variable?
    private void Awake()
    {
        instance = this;
    }
    #endregion 

    public GameObject player;
}
