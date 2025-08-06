using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    [SerializeField] public GameObject subtitleT;
    [SerializeField] public GameObject SubtitleTrigge;
    [SerializeField] GameObject GoEast;

    public TMPTypewriterEffect1 subtitleManager;

    [TextArea] public string dialogue = "Hello, this is your subtitle...";
    [TextArea] public string dialogue2 = "And this is the second part.";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft"))

        {
            GoEast.SetActive(false);
            subtitleT.SetActive(true);
            subtitleManager.StartSequentialTyping(dialogue, dialogue2, subtitleT);
            SubtitleTrigge.SetActive(false);
        }
    }

}
