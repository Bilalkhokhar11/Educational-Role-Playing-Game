using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrsaDIsable : MonoBehaviour
{
    [SerializeField] GameObject UrsaMinor;
    [SerializeField] GameObject L1Trigger;
    [SerializeField] GameObject PolSch;

    // Start is called before the first frame update
    private void Start()
    {
        PolSch.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        UrsaMinor.SetActive(false);
        L1Trigger.SetActive(false);
    }
}
