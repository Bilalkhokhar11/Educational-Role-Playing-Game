using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RMM : MonoBehaviour
{
    // Start is called before the first frame update
  
    public void OnBackToMainMenuClick()
    {
        Time.timeScale = 1f; // Ensure game resumes before switching
        SceneManager.LoadScene(0); // Main Menu is scene index 0
    }
}
