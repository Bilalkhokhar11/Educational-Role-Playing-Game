using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class croplistappear : MonoBehaviour
{
    [SerializeField] GameObject croplist;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            croplist.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}