using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bzin : MonoBehaviour
{
    [SerializeField] GameObject buggy;
    // Start is called before the first frame update
    
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buggy.SetActive(false);
        }

    }
}
