using UnityEngine;
using System.Collections;

public class SequentialFade : MonoBehaviour
{
    public CanvasGroup bgGroup;     // Background frame
    public CanvasGroup imageGroup;  // Image on top
    public float fadeDuration = 1f;
    public float delayBetween = 0.3f;

    void Start()
    {
        StartCoroutine(FadeInSequence());
    }

    IEnumerator FadeInSequence()
    {
        yield return StartCoroutine(FadeCanvasGroup(bgGroup, fadeDuration));
        yield return new WaitForSeconds(delayBetween);
        yield return StartCoroutine(FadeCanvasGroup(imageGroup, fadeDuration));
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
