using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2ObjectiveTrigger : MonoBehaviour
   
{
    [SerializeField] GameObject Lesson1;
    [SerializeField] private GameObject UrsaMinor;
    [SerializeField] private GameObject Lesson2Objective;
    [SerializeField]  GameObject Lesson2;
    // Start is called before the first frame update
    void Start()
    { 
        UrsaMinor.SetActive(false);
        Lesson2Objective.SetActive(false);
        Lesson2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Raft"))

        {
            Lesson2Objective.SetActive(true);
            UrsaMinor.SetActive(true) ;
            Lesson1.SetActive(false);
            Lesson2.SetActive(true);
        }

    }
}
