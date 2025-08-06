using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowImageOnClick : MonoBehaviour
{
    [SerializeField] private GameObject previousImg;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject imageToShow;
    [SerializeField] private GameObject delayedObject;
    [SerializeField] private float delaySeconds = 7f;

    private void Start()
    {
        // Hook up the button and ensure everything starts hidden
        nextButton.onClick.AddListener(ShowImage);
        imageToShow.SetActive(false);
        if (delayedObject != null)
            delayedObject.SetActive(false);
    }
    

    private void ShowImage()
    {
        // Hide previous UI, show main image, then kick off real-time delay
        previousImg.SetActive(false);
        nextButton.gameObject.SetActive(false);
        imageToShow.SetActive(true);
        StartCoroutine(ActivateAfterRealTimeDelay(delaySeconds));
    }

    private IEnumerator ActivateAfterRealTimeDelay(float delay)
    {
        // Wait using unscaled time so it still fires while paused
        yield return new WaitForSecondsRealtime(delay);

        if (delayedObject != null)
            delayedObject.SetActive(true);
    }
}
