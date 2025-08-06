using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    private GameObject UrsaMinor;
    // Start is called before the first frame update
    void Start()
    {
        UrsaMinor = GameObject.Find("Ursa Minor");
       GameObject.Find("UrsaMinor").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
