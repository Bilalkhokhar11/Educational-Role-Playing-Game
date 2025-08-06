using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class L2TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float charactersPerSecond = 50f;
    public AudioClip audioClip;
    public AudioSource audioSource;
    private Coroutine typingCoroutine;

    void Start()
    {
        GameObject.Find("SubtitleT").SetActive(false);
    }

    public void StartSequentialTyping(string message1, string message2, GameObject objectToDisable)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeTwoTexts(message1, message2, objectToDisable));
    }

    private IEnumerator TypeTwoTexts(string message1, string message2, GameObject objectToDisable)
    {
        yield return StartCoroutine(TypeText(message1));
        yield return new WaitForSecondsRealtime (0.5f);
        yield return StartCoroutine(TypeText(message2));
        yield return new WaitForSecondsRealtime(3f);

        if (objectToDisable != null)
            objectToDisable.SetActive(false);
    }

    private IEnumerator TypeText(string message)
    {
        textUI.text = message;
        textUI.maxVisibleCharacters = 0;

        float delay = 1f / charactersPerSecond;
        for (int i = 0; i <= message.Length; i++)
        {
            textUI.maxVisibleCharacters = i;
            if (audioClip && audioSource) audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(delay);
        }
    }
}
