using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class a : MonoBehaviour
{
    [SerializeField] Button aoption;
    public float delayBe = 1.3f;
    [SerializeField] GameObject Correct;
    [SerializeField] GameObject Proceed;
    [SerializeField] GameObject enkibbbgroup;
    float delayB;
    // Start is called before the first frame update

    void Start()
    {
        Correct.SetActive(false);
        aoption.interactable = true;
        aoption.onClick.AddListener(ReturntoGed);
        Proceed.SetActive(false);

    }
    void ReturntoGed()
    {
        StartCoroutine(Proceede());

    }
    IEnumerator Proceede()
    {
        Correct.SetActive(true);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(delayBe);
        enkibbbgroup.SetActive(false);
        yield return new WaitForSeconds(delayB);
        Proceed.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}