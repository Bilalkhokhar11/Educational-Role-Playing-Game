using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Drawing;


public class SubtitleManager : MonoBehaviour
{
    public GameObject listenconvo;
   // public Text TextToFade;        // The Image component inside listenconvo
    public TextMeshProUGUI imageToFade;        // The Image component inside listenconvo

    public float fadeDuration = 2f;
    [System.Serializable]
    public class DialogueLine
    {
        public TextMeshProUGUI speakerName;
        [TextArea(2, 5)] public string text;
        public float duration = 2f;
        public UnityEngine.Color textColor;
        public Sprite portrait;
    }

    public GameObject subtitlePanel;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;

    public float typingSpeed = 0.03f;

    public List<DialogueLine> dialogueLines;

    void Start()
    {
        subtitlePanel.SetActive(false);
        listenconvo.SetActive(false);

    }

    public void StartDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        subtitlePanel.SetActive(true);
        listenconvo.SetActive(true);
        UnityEngine.Color color = imageToFade.color;
        yield return new WaitForSeconds(3f);

        color.a = 1f;
        imageToFade.color = color;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            imageToFade.color = color;
            yield return null;
        }

        color.a = 0f;
        imageToFade.color = color;

        listenconvo.SetActive(false);

        foreach (var line in dialogueLines)
        {
            dialogueText.color = line.textColor;
            portraitImage.sprite = line.portrait;
            yield return StartCoroutine(TypeText(line.text));
            yield return new WaitForSeconds(line.duration);
        }

        subtitlePanel.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {   

        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
