using UnityEngine;
using System.Collections;

public class UIFadeOnTrigger : MonoBehaviour
{
    public CanvasGroup bgGroup;     // Background frame
    public CanvasGroup imageGroup;  // Foreground image
    public float fadeDuration = 1f;
    public float delayBetween = 0.3f;
    private bool hasFaded = false;
    [SerializeField] GameObject SubtitleManager;
    void OnTriggerEnter(Collider other)
    {
        // Optional: Use tag check to filter what triggers the fade
        if (!hasFaded && other.CompareTag("Raft"))
        {
            
            SubtitleManager
                .SetActive(false);
            hasFaded = true;
            StartCoroutine(FadeInSequence());
        }
    }

    IEnumerator FadeInSequence()
    {
        yield return StartCoroutine(FadeCanvasGroup(bgGroup, fadeDuration));
        yield return new WaitForSeconds(delayBetween);
        yield return StartCoroutine(FadeCanvasGroup(imageGroup, fadeDuration));

        
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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