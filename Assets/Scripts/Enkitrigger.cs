using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enkitrigger : MonoBehaviour
{
    public CanvasGroup bgGroup;     // Background frame
    public CanvasGroup imageGroup;  // Foreground image
    public float fadeDuration = 1f;
    [SerializeField] private float delaySeconds = 7f;
    [SerializeField] GameObject l2c;
    [SerializeField] GameObject SUBTRIGGER4;
    [SerializeField] GameObject enkiquiz;
    public float delayBetween = 0.3f;
    // Start is called before the first frame update
    private void Start()
    {
        l2c.SetActive(false);
        enkiquiz.SetActive
                (false);
    }
    void OnTriggerEnter(Collider other)
    {
        // Optional: Use tag check to filter what triggers the fade
        if (other.CompareTag("Raft"))
        {
            enkiquiz.SetActive
                (true);
            StartCoroutine(FadeInSequence());
            l2c.SetActive(false);
            SUBTRIGGER4.SetActive(false);

        }
        IEnumerator FadeInSequence()
        {
            
UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            yield return StartCoroutine(FadeCanvasGroup(bgGroup, fadeDuration));
            yield return new WaitForSeconds(delayBetween);
            yield return StartCoroutine(FadeCanvasGroup(imageGroup, fadeDuration));
            yield return new WaitForSecondsRealtime(delaySeconds);

            



        }


        IEnumerator FadeCanvasGroup(CanvasGroup group, float duration)
        {
            float elapsed = 0f;
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                group.alpha = Mathf.Clamp01(elapsed / duration);
                yield return null;
            }

            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }
}