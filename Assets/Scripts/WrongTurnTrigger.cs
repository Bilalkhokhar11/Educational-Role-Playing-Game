using System.Collections;
using UnityEngine;

public class WrongTurnTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject Youtookwrongturn;
    [SerializeField] private float messageDelay = 1f;
    [SerializeField] private float respawnDelay = 2f;

    private Transform objectToRespawn;

    private void Start()
    {
        // Auto-find the object tagged "Raft"
        GameObject raft = GameObject.FindGameObjectWithTag("Raft");
        if (raft != null)
        {
            objectToRespawn = raft.transform;
        }
        else
        {
            Debug.LogError("No object with tag 'Raft' found in the scene.");
        }

        if (Youtookwrongturn != null)
            Youtookwrongturn.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft"))
        {
            StartCoroutine(RespawnSequence());
        }
    }

    private IEnumerator RespawnSequence()
    {
        yield return new WaitForSeconds(messageDelay);

        if (Youtookwrongturn != null)
            Youtookwrongturn.SetActive(true);

        yield return new WaitForSeconds(respawnDelay);

        if (objectToRespawn != null && spawnPoint != null)
        {
            objectToRespawn.position = spawnPoint.position;
            objectToRespawn.rotation = spawnPoint.rotation;

            // Reset Rigidbody velocity if applicable
            Rigidbody rb = objectToRespawn.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}