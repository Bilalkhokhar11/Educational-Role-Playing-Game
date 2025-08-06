using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitiateCtesiphone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Mainmenus;
    [SerializeField] GameObject Chaptrs;
    [SerializeField] Button Chapter1;
    [SerializeField] GameObject LoadingScreen;
    private float delayf = 15f;
    private float delays = 3f;
    [SerializeField] GameObject Chaptersss;
    
    void Start()
    {
        Chapter1.onClick.AddListener(LoadSceneone);
        LoadingScreen.SetActive(false);
    }
    IEnumerator LoadSceneone4()
    {
        LoadingScreen.SetActive(true);

        yield return new WaitForSeconds(delayf); 
        SceneManager.LoadScene("Ctesiphon");
        yield return new WaitForSeconds(delays);
        LoadingScreen.SetActive (false);

                Chaptersss.SetActive(false );



    }
    void LoadSceneone()
    {
        StartCoroutine(LoadSceneone4());


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

