using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resumebutton : MonoBehaviour
{
    [SerializeField] Button resumekey;
    [SerializeField] GameObject pausemenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        resumekey.onClick.AddListener(returntogame);

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
    }
    void returntogame()
    {
        pausemenu.SetActive(false);

        Time.timeScale = 1f;


    }
}
