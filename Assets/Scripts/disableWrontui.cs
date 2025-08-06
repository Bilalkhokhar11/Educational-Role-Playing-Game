using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class disableWrontui : MonoBehaviour
{
    [SerializeField] GameObject wrongturnui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
         void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Raft"))
            {
                wrongturnui.SetActive(false);
            }
        }
    }
