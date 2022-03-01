using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killparticles : MonoBehaviour
{
    public float LifeTime;
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}
