using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Appearcroplis : MonoBehaviour
{
    [SerializeField] GameObject Croplist;
    [SerializeField] GameObject youfailed;
    [SerializeField] public CameraController cameraController;
    // Start is called before the first frame update
    private List<Graphic> uiGraphics = new List<Graphic>();

    private void Start()
    {
        youfailed.SetActive(false);
        // Collect all UI Graphic components inside Croplist (Image, Text, TMP, etc.)
        if (Croplist != null)
        {
            Croplist.SetActive(false); // Hide at start

            uiGraphics.AddRange(Croplist.GetComponentsInChildren<Image>(true));
            uiGraphics.AddRange(Croplist.GetComponentsInChildren<Text>(true));
            uiGraphics.AddRange(Croplist.GetComponentsInChildren<TextMeshProUGUI>(true));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerSequence());
        }
    }

    private IEnumerator TriggerSequence()
    {
        // Step 0: Pause game time
        Time.timeScale = 0f;

        // Step 1: Camera zoom
        CameraController camController = Camera.main.GetComponent<CameraController>();
        camController.distance = -0.5f;

        // Step 2: Wait 2 seconds in real time
        yield return new WaitForSecondsRealtime(4f);

        Croplist.SetActive(true);

        // Fade-in setup: Set all alpha to 0
        foreach (var g in uiGraphics)
        {
            Color c = g.color;
            c.a = 0f;
            g.color = c;
        }

        float duration = 1f;
        float elapsed = 0f;

        // Step 2 continued: Fade in
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);

            foreach (var g in uiGraphics)
            {
                Color c = g.color;
                c.a = alpha;
                g.color = c;
            }

            yield return null;
        }

        // Step 3: After 0.5 sec, show cursor
        yield return new WaitForSecondsRealtime(0.5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}