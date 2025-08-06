using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bzout : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject buggy;
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buggy.SetActive(true);
        }

    }
}
