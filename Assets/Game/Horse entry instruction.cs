using MalbersAnimations;
using UnityEngine;

public class HorseEntryInstruction : MonoBehaviour
{
    [SerializeField] private GameObject instructObject;
    [SerializeField] private GameObject objA;
    [SerializeField] private GameObject objB;
    [SerializeField] public CameraController cameraController;
    [SerializeField] GameObject entryObjective;
    [SerializeField] GameObject MC;
    [SerializeField] GameObject HorseS;
    [SerializeField] GameObject NPC;
    private bool playerInside = false;


    private void Awake()
    {
        if (instructObject != null)
            instructObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered trigger zone.");

            // Only show instructions if objA is NOT already a child of objB
            if (instructObject != null && !(objA != null && objA.transform.parent == objB.transform))
                instructObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player exited trigger zone.");
            
            if (instructObject != null)
                instructObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInside && objA != null && objB != null && objA.transform.parent == objB.transform)
        {
            if (instructObject != null && instructObject.activeSelf)
            {
                CameraController camController = Camera.main.GetComponent<CameraController>();
                float dist = camController.distance = 12f;
                float minAngle = camController.minVerticalAngle = 25f;
                entryObjective.SetActive(true);
                instructObject.SetActive(false);
                Debug.Log("objA became child of objB while player was in zone, instruction hidden.");
            }

           
        }
        else
        {
            CameraController camController = Camera.main.GetComponent<CameraController>();
            float dist = camController.distance = 3.8f;
            camController.followTarget = MC.transform;
            float minAngle = camController.minVerticalAngle = -25f;
            entryObjective.SetActive(false);

        }
    }
}
