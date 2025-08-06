using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class c : MonoBehaviour
{
    [SerializeField] Button coption;
    [SerializeField] GameObject youfailed;
    public float delayBetween = 2f;
    public float delayBe = 1f;
    public float delayres = 2f;
    [SerializeField] GameObject wronAnswer;
    [SerializeField] GameObject enkibbbgroup;
   

    // Start is called before the first frame update
    void Start()
    {
        coption.onClick.AddListener(ReturntoGq);
        youfailed.SetActive(false);
        wronAnswer.SetActive(false);

    }


    // Update is called once per frame
    void ReturntoGq()
    {

        StartCoroutine(RestartLevel());

    }
    IEnumerator RestartLevel()
    {

        wronAnswer.SetActive(true);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(delayBe);
        enkibbbgroup.SetActive(false);


        yield return new WaitForSeconds(delayBetween);
        youfailed.SetActive(true);
        yield return new WaitForSeconds(delayres);



        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
