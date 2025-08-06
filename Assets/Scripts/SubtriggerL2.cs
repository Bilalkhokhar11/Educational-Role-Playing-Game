using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtriggerL2 : MonoBehaviour
{
    [SerializeField] public GameObject subtitleT;

    public TMPTypewriterEffect subtitleManager;

    [TextArea] public string dialogue = "Hello, this is your subtitle...";
    [TextArea] public string dialogue2 = "And this is the second part.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft"))
        {
            subtitleT.SetActive(true);
            subtitleManager.StartSequentialTyping(dialogue, dialogue2, subtitleT);
            
        }
    }

}
