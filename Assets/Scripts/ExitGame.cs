using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    [SerializeField] Button ExitGames;
    // This method will be called when the button is clicked
    void Start()
    {
      ExitGames.interactable = true;

        ExitGames.onClick.RemoveAllListeners();
        ExitGames.onClick.AddListener(Endprogram);

    }
   void Endprogram()
    {
        Debug.Log("Exit button clicked. Quitting the game...");

        Application.Quit();

#if UNITY_EDITOR
        // Stop play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
