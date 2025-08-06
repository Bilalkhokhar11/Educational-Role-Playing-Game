using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitiateSusa : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Mainmenus;
    [SerializeField] GameObject Chaptrs;
    [SerializeField] Button Chapter2;
    [SerializeField] GameObject LoadingScreen;
    private float delayf = 15f;
    private float delays = 3f;
    [SerializeField] GameObject Chaptersss;

    void Start()
    {
        Chapter2.onClick.AddListener(LoadSceneTwo1);
        LoadingScreen.SetActive(false);
    }
    IEnumerator LoadSceneTwo()
    {
        LoadingScreen.SetActive(true);
        yield return new WaitForSeconds(delayf);
        SceneManager.LoadScene("Susa");
        yield return new WaitForSeconds(delays);
        LoadingScreen.SetActive(false);

        Chaptersss.SetActive(false);



    }
    void LoadSceneTwo1()
    {

        StartCoroutine(LoadSceneTwo());

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) // or KeyCode.KeypadEnter for numpad
        {
            if (Mainmenus != null)
                Mainmenus.SetActive(true);

            if (Chaptrs != null)
                Chaptrs.SetActive(false);
        }
    }
}
