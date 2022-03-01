using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSAnimEvent : MonoBehaviour
{
    public RespawnCD ReCDscript;
    public GameObject QuitButton;

    public void DeathScreenCD()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ReCDscript.enabled = true;
    }
}