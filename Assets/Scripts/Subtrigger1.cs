using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subtrigger1 : MonoBehaviour

{
    [SerializeField] public GameObject subtitleTR;
    [SerializeField] public GameObject SubtitleTrigger1;

    public TMPTypewriterEffect2 subtitleManager11;

    [TextArea] public string dialogue = "Hello, this is your subtitle...";
    [TextArea] public string dialogue2 = "And this is the second part.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft"))
        {
            subtitleTR.SetActive(true);
            subtitleManager11.StartSequentialTyping(dialogue, dialogue2, subtitleTR);
            SubtitleTrigger1.SetActive(false);
        }
    }

}
