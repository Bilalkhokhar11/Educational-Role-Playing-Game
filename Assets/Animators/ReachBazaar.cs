using MalbersAnimations.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachBazaar : MonoBehaviour
{
    [SerializeField] GameObject Bazarwalla;
    [SerializeField] GameObject objA;
    [SerializeField] GameObject objB;
    [SerializeField] GameObject mounterss;
    [SerializeField] GameObject blipstuff1;
    [SerializeField] GameObject Bazararrivalinstruction;
    [SerializeField] GameObject exitcarraigeIns;
    [SerializeField] GameObject getseed;
    [SerializeField] GameObject subpanel;
    [SerializeField] GameObject submanager;
    [SerializeField] GameObject croplist;
    private void Start()
    {
        Bazarwalla.SetActive(true);
        Bazarwalla.SetActive(true);
        Bazararrivalinstruction.SetActive(false);
        exitcarraigeIns.SetActive(false);
        getseed.SetActive(false);
        croplist.SetActive(false);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            
            submanager.SetActive(false);
            subpanel.SetActive(false);  
            Bazararrivalinstruction.SetActive(true);
            exitcarraigeIns.SetActive(true);
           
            if (objA.transform.parent != objB.transform)
            {
                mounterss.SetActive(false);
                Debug.Log("objA is NOT a direct child of objB");
                Bazarwalla.SetActive(false);
                exitcarraigeIns.SetActive(false);
                blipstuff1.SetActive(false);
                getseed.SetActive(true);
                Bazararrivalinstruction.SetActive(false);

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            Bazararrivalinstruction.SetActive(false);
            exitcarraigeIns.SetActive(false);
        }
    }
}