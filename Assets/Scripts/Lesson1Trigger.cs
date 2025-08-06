using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1Trigger : MonoBehaviour
{
    [SerializeField] public GameObject Lesson1Triger;
    [SerializeField] private GameObject Lesson1;
    [SerializeField] private GameObject SubtitleManager;
    // Start is called before the first frame update
    void Start()
    {
        Lesson1Triger.SetActive(true);
        Lesson1.SetActive(false);
    }
     private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Raft"))
        {
          Time.timeScale = 0f; // Pauses the game

            SubtitleManager.SetActive(false);
            Lesson1.SetActive(true);
            Lesson1Triger.SetActive(false);
        }
    }

}


