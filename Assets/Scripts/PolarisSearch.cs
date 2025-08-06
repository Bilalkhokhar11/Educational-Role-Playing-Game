using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PolarisSearch : MonoBehaviour
{
    [SerializeField] CameraController1 cameraController1;
    [SerializeField] GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        {
            cameraController1.distance = -1f;
            // Continuously check for Ctrl input
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (cameraController1 != null)
                    cameraController1.enabled = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (cameraController1 != null)
                    cameraController1.enabled = true;
            }
        }

    }

}

