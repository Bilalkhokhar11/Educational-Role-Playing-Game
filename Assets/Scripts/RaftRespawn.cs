using UnityEngine;

public class RaftRespawner : MonoBehaviour
{
    [Tooltip("Where to teleport")]
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnZone"))
        {
            // immediate teleport
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            Debug.Log("RaftRespawner: Teleported!");
        }
    }
}
