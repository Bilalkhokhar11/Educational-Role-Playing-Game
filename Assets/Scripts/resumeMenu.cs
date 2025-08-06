using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resumeMenu : MonoBehaviour
{
    public PauseMenuManager pauseMenu;

    public void OnResumeButtonClick()
    {
        pauseMenu.Resume();
    }
}