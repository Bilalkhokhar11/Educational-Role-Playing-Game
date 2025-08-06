using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public float rayDistance = 10000f;
    public LayerMask clickMask; // Optional: assign this in Inspector

    void Update()
    {
        // Check if Left Mouse Button is clicked while holding Ctrl
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, clickMask))
            {
                ClickableTarget target = hit.collider.GetComponent<ClickableTarget>();
                if (target != null)
                {
                    target.TriggerAction();
                }
                else
                {
                    Debug.Log("Hit object has no ClickableTarget component.");
                }
            }
            else
            {
                Debug.Log("Nothing hit by raycast.");
            }
        }
    }
}
