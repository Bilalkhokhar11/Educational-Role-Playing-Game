using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickableTarget : MonoBehaviour
{
   
    [SerializeField] GameObject UrsaCons;
    [SerializeField] GameObject PolarisSearchInstruction;
    [SerializeField] GameObject Lesson2Trigger;
    [SerializeField] GameObject PolarisSearch;
    [SerializeField] CameraController1 cameraController1;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject ObjectiveLesson2;


    
    public CanvasGroup bgGroup;     // Background frame
    public CanvasGroup imageGroup;  // Foreground image
    public float fadeDuration = 1f;
    public float delayBetween = 0.3f;
    [SerializeField] private float delaySeconds = 7f;
    [SerializeField] GameObject delayedObject;
    [SerializeField] GameObject l2c;
   // private Animator playerAnimator;

    private void Start()
    {            delayedObject.SetActive(false);

    }
    public void TriggerAction()
    {


        
        /*raftController.GetComponent<RaftController>().enabled= true;
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.enabled = false;*/
        StartCoroutine(FadeInSequence());

        IEnumerator FadeInSequence()
        {
            l2c.SetActive(true);
        UrsaCons.SetActive(true);
            yield return new WaitForSeconds(delayBetween);
        PolarisSearchInstruction.SetActive(false);
        PolarisSearch.SetActive(false);
        Lesson2Trigger.SetActive(false);
        cameraController1.enabled = true;
        cameraController1.distance = 6.5f;
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        ObjectiveLesson2.SetActive(true);
            yield return StartCoroutine(FadeCanvasGroup(bgGroup, fadeDuration));
            yield return new WaitForSeconds(delayBetween);
            yield return StartCoroutine(FadeCanvasGroup(imageGroup, fadeDuration));
            yield return new WaitForSecondsRealtime(delaySeconds);

            yield return StartCoroutine(ActivateAfterRealTimeDelay(delaySeconds));
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;



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
        IEnumerator ActivateAfterRealTimeDelay(float delay)
        {
            // Wait using unscaled time so it still fires while paused
            yield return new WaitForSecondsRealtime(delay);

            if (delayedObject != null)
            { //UnityEngine.Cursor.visible = true;
                delayedObject.SetActive(true);
            }
        }

    }
}
