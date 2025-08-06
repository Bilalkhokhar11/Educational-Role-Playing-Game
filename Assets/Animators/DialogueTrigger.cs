using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject animal;
    public SubtitleManager subtitleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            subtitleManager.StartDialogue();
            GetComponent<Collider>().enabled = false; // prevent retrigger
        }
    }
}
