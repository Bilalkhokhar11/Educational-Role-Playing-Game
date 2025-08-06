using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    [SerializeField] GameObject Chapters;
    [SerializeField] Button PlaytGame;
    [SerializeField] GameObject Mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        if (PlaytGame == null || Chapters == null || Mainmenu == null)
        {
            Debug.LogError("Missing references!");
            return;
        }

        Chapters.SetActive(false);
        PlaytGame.onClick.RemoveAllListeners();
        PlaytGame.onClick.AddListener(InitiateChapters);

        StartCoroutine(EnablePlayButtonAfterDelay());
    }
    IEnumerator EnablePlayButtonAfterDelay()
    {
        PlaytGame.interactable = false;
        yield return null; // wait 1 frame
        PlaytGame.interactable = true;
    }



    // Update is called once per frame
    void InitiateChapters()
    {
        Chapters.SetActive(true);


        Mainmenu.SetActive(false);

    }
}
