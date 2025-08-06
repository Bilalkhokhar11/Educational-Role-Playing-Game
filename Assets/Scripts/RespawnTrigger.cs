using System.Collections;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject wrongTurnUI;
    [SerializeField] private float uiDelay = 1f;
    [SerializeField] private float respawnDelay = 2f;

    private Transform raftTransform;
    private CharacterController charController;
    private Rigidbody rb;
    private bool rbWasKinematic;

    private void Start()
    {
        // Find the raft by tag
        GameObject raftGO = GameObject.FindGameObjectWithTag("Raft");
        if (raftGO == null)
        {
            Debug.LogError("[RespawnTrigger] No GameObject tagged 'Raft' found.");
            enabled = false;
            return;
        }

        raftTransform = raftGO.transform;
        charController = raftGO.GetComponent<CharacterController>();
        rb = raftGO.GetComponent<Rigidbody>();
        if (rb != null) rbWasKinematic = rb.isKinematic;

        if (wrongTurnUI != null)
            wrongTurnUI.SetActive(false);

        if (spawnPoint == null)
            Debug.LogError("[RespawnTrigger] spawnPoint not assigned.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft"))
            StartCoroutine(DoRespawnSequence());
    }

    private IEnumerator DoRespawnSequence()
    {
        // 1) Delay before UI
        yield return new WaitForSeconds(uiDelay);
        if (wrongTurnUI != null)
            wrongTurnUI.SetActive(true);

        // 2) Delay before actual teleport
        yield return new WaitForSeconds(respawnDelay);

        // 3) Disable controllers/physics overrides
        if (charController != null) charController.enabled = false;
        if (rb != null && !rbWasKinematic) rb.isKinematic = true;

        // 4) Teleport
        raftTransform.position = spawnPoint.position;
        raftTransform.rotation = spawnPoint.rotation;

        // 5) Restore controllers/physics
        if (charController != null) charController.enabled = true;
        if (rb != null) rb.isKinematic = rbWasKinematic;

        // 6) Reset any velocities
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        wrongTurnUI.SetActive(false );
    }
}
