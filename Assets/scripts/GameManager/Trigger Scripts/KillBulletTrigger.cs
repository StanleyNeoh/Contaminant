using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBulletTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<bulletphy>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
