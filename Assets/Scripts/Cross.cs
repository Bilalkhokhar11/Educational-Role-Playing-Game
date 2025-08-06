using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour
{
    [SerializeField] private GameObject story;
    [SerializeField] private Button resumeButton;

    private void Start()
    {
        // Ensure only one listener is attached
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(OnResumeClicked);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnResumeClicked()
    {
        Time.timeScale = 1f;

        if (story != null)
        {
            story.SetActive(false);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}