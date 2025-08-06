using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lesson2Trigger : MonoBehaviour
{
    public CanvasGroup bgGroup;     // Background frame
    public CanvasGroup imageGroup;  // Foreground image
    public float fadeDuration = 1f;
    public float delayBetween = 0.3f;
    [SerializeField] GameObject delayedObject;
    private bool hasFaded = false;
    [SerializeField] GameObject Lesson2Obj;
    [SerializeField] private float delaySeconds = 7f;
    [SerializeField] GameObject Sub2Manager;
    [SerializeField] private GameObject raftObject;
    [SerializeField] private GameObject player;
    [SerializeField] GameObject L2Trigger;
    private RaftController raftController;
    private Animator playerAnimator;
    [SerializeField] GameObject L2Objb;
    [SerializeField] GameObject polarisGamemanager;
    private void Start()
    {
        polarisGamemanager.SetActive(false);
        L2Objb.SetActive(false);
        if (delayedObject != null)
            delayedObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        // Optional: Use tag check to filter what triggers the fade
        if (!hasFaded && other.CompareTag("Raft"))
        {
            if (raftObject != null && player != null)
            {
                raftController = raftObject.GetComponent<RaftController>();
                playerAnimator = player.GetComponent<Animator>();
                if (raftObject != null && player != null)
                {
                   playerAnimator.enabled = false;
                    raftController.enabled = false;
                }
            }
            StartCoroutine(PlaySubtitleSequence());  // Real-time subtitles
            hasFaded = true;
            
        }
    }
    IEnumerator PlaySubtitleSequence()
    {
        
        Sub2Manager.SetActive(true);   // Subtitle box shows up
        yield return new WaitForSeconds(6f);
        yield return new WaitForSeconds(10f);

        StartCoroutine(FadeInSequence());

    }


    IEnumerator FadeInSequence()
    {


        yield return StartCoroutine(FadeCanvasGroup(bgGroup, fadeDuration));
        yield return new WaitForSeconds(delayBetween);
        yield return StartCoroutine(FadeCanvasGroup(imageGroup, fadeDuration));
                yield return new WaitForSecondsRealtime(delaySeconds);

        yield return StartCoroutine(ActivateAfterRealTimeDelay(delaySeconds));
        UnityEngine.Cursor.visible = true;

        
         
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
    private IEnumerator ActivateAfterRealTimeDelay(float delay)
    {
        // Wait using unscaled time so it still fires while paused
        yield return new WaitForSecondsRealtime(delay);

        if (delayedObject != null) 
        { //UnityEngine.Cursor.visible = true;
            delayedObject.SetActive(true);}
        L2Trigger.SetActive(false);
        polarisGamemanager.SetActive(true);
          
    }


}