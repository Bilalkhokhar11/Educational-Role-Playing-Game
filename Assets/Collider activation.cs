using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideractivation : MonoBehaviour
{
    private CapsuleCollider capsule;

    void Start()
    {
        capsule = GetComponent<CapsuleCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (capsule != null && !capsule.enabled)
        {
            Debug.Log("Collider was disabled. Re-enabling it.");
            capsule.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (capsule != null && !capsule.enabled)
        {
            Debug.Log("Collider was disabled in trigger. Re-enabling it.");
            capsule.enabled = true;
        }
    }
}
