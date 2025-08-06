using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstory : MonoBehaviour
{
    [SerializeField] private GameObject story;

    private void Start()
    {
        if (story != null)
        {
            story.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(HideStoryAfterDelay());
    }

    private IEnumerator HideStoryAfterDelay()
    {
        yield return new WaitForSecondsRealtime(15f); // waits 15 real-time seconds, not affected by Time.timeScale

        if (story != null)
        {
            story.SetActive(false);
        }

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}