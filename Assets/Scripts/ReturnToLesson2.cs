using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ReturnToLesson2 : MonoBehaviour
{
    [SerializeField] Button DoneButton2;
    [SerializeField] GameObject Lesson2;
    [SerializeField] GameObject PolarisSearch;
    [SerializeField] GameObject PolSearch;
    [SerializeField] GameObject L2ObjA;
    [SerializeField] GameObject L2ObjB;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlocks the cursor

        Cursor.visible = true;
        DoneButton2.onClick.AddListener(ReturntoG);
        PolarisSearch.SetActive(false);

    }

    // Update is called once per frame
    void ReturntoG()
    {
        Cursor.visible
            = false;
        Cursor.lockState = CursorLockMode.Locked;       
        PolarisSearch.SetActive(true);
        Time.timeScale = 1f;
        PolSearch.SetActive(true
            ) ; 
        L2ObjA.SetActive(false);
        Lesson2.SetActive(false);
       
    }
}