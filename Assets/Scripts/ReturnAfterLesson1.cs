using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReturnAfterLesson1 : MonoBehaviour
{
  [SerializeField] Button DoneButton;
    [SerializeField] GameObject Lesson1;
    private bool isGameResumed = false;
    [SerializeField] GameObject L1trigger;

    private void Start()
    {
        DoneButton.onClick.AddListener(ReturntoG);
    }

    // Start is called before the first frame update
    void ReturntoG()
    {
        Time.timeScale = 1f;
        Lesson1.SetActive(false);
        isGameResumed = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        L1trigger.SetActive(false);
    }

    void  OnApplicationFocus (bool hasFocus)
    {
        if (hasFocus && isGameResumed)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
