using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Proceed : MonoBehaviour
{
    [SerializeField] Button proceed;
    // Start is called before the first frame update
    void Start()
    {
       proceed.interactable = true;
       proceed.onClick.AddListener(tomainmenu);

    }

    // Update is called once per frame
    void tomainmenu()
    {
        StartCoroutine(Backtomainmenu());

    }
    IEnumerator Backtomainmenu()
    {
        yield return null;

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
