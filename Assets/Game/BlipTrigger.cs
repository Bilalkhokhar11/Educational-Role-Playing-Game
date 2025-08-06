using UnityEngine;
using System;
using UnityEditor;
using System.Linq;
public class BlipTrigger : MonoBehaviour
{
    public GameObject Bilp;
    [SerializeField] private GameObject[] HelpQ;
    public NPCMovement npcController;
    bool isPlayerInside;
    [SerializeField] GameObject reachblip;
    [SerializeField] GameObject Gettomark;
    [SerializeField] GameObject mounters;
    private void Start()
    {
       mounters.SetActive(false);   
        foreach (GameObject obj in HelpQ)
        {
            if (obj != null)
                obj.SetActive(false);

        }
        Gettomark.SetActive(false);
        reachblip.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in HelpQ)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in HelpQ)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && HelpQ.All(obj => obj != null && obj.activeSelf) && Input.GetKeyDown(KeyCode.Return))
        {
            Gettomark.SetActive(true);
            reachblip.SetActive(true);
            mounters.SetActive(true);
            npcController.StartWalking();
            GetComponent<Collider>().enabled = false; // Disable after trigger
            foreach (GameObject obj in HelpQ)
            {
                if (obj != null)
                    obj.SetActive(false);
            }
            isPlayerInside = false;
            Debug.Log("Object is active and Enter was pressed!");
            Bilp.SetActive(false);
        }
        
    }
}