using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoneCrop : MonoBehaviour
{
    public GameObject cropList;
    [SerializeField] private Button done;
    [SerializeField] GameObject blip2;

    private void Start()
    {
        done.onClick.RemoveAllListeners();
        done.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (cropList != null)
        {
            cropList.SetActive(false);
        }

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cropList.SetActive(false);
        blip2.SetActive(false); 
    }
}
