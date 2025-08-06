using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button mainMenuButton;

    private static bool isGamePaused = false;

    void Start()
    {
        // Ensure buttons exist
        if (resumeButton != null)
        {
            resumeButton.interactable = true;
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(Resume);
        }
        else
        {
            Debug.LogError("Resume Button not assigned!");
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.interactable = true;
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(LoadMainMenu);
        }
        else
        {
            Debug.LogError("Main Menu Button not assigned!");
        }

        // Make sure pause menu is disabled initially
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        else
            Debug.LogError("Pause Menu UI not assigned!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Assumes main menu is scene 0
    }
}