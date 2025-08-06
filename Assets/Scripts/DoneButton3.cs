using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneButton3 : MonoBehaviour
{
    [SerializeField] Button DoneButton3x;
    [SerializeField] RaftController raftController;
     [SerializeField] GameObject Raft;
     [SerializeField] private GameObject player;
     private Animator playerAnimator;
    [SerializeField] ClickableTarget clickableTarget;
    [SerializeField] GameObject l2c;
    [SerializeField] GameObject sphere6;


    // Start is called before the first frame update
    void Start()
    {
        DoneButton3x.onClick.AddListener(ReturntoGw);

    }

    // Update is called once per frame
    void ReturntoGw()
    {

        raftController.GetComponent<RaftController>().enabled= true;
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.enabled = true;
        clickableTarget.GetComponent<ClickableTarget>().enabled = false;
        l2c.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
