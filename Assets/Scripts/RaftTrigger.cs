using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftTrigger : MonoBehaviour
{
    [SerializeField] private RaftController raftController;
    private GameObject raftEnterControl;

    void Start()
    {
        raftEnterControl = GameObject.Find("RaftEnterControl");
        GameObject.Find("RaftEnterControl").SetActive(false);

        if (raftEnterControl == null)
        {
            Debug.LogError("RaftEnterControl not found! Check spelling and hierarchy!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && raftEnterControl != null)
        {
            raftEnterControl.SetActive(true); // ? Enable when Player enters
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && raftEnterControl != null)
        {
            raftEnterControl.SetActive(false); // ? Disable when Player exits
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetButton("EnterRaft"))
        {
            raftEnterControl.SetActive(false);
            GameObject canvas = GameObject.Find("Canvas22");
            if (canvas != null)
            {
                canvas.SetActive(false);
            }

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.EnterRaft(raftController);
                raftController.SetPlayerOnBoard(true, player);
            }
        }
    }
}
