using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Header("References")]
    public Transform wagonWalkTarget; // Point near wagon to walk to
    public Transform wagonSitPosition; // Sit point inside wagon
    public float moveSpeed = 3f;
    public float stoppingDistance = 1f;
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject Wagon;
    private Animator animator;
    private bool isWalking = false;
    private bool isSeated = false;
        public Transform player; // Assign the player transform in Inspector
        private Quaternion originalRotation;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        // ✅ Only face player if not walking to wagon
        if (!isWalking)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Lock the Y-axis so the NPC doesn't tilt up/down

            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
        }

        // ✅ Move to wagon if walking is active and not seated
        if (isWalking && !isSeated)
        {
            MoveToWagon();
        }
    }

    // Called by trigger/player input to start walking
    public void StartWalking()
    {
        if (!isSeated)
        {
            isWalking = true;
            animator.SetBool("IsWalking", true);
        }
    }

    private void MoveToWagon()
    {
        // Calculate direction to target
        Vector3 direction = wagonWalkTarget.position - transform.position;
        direction.y = 0; // Ignore vertical

        // Rotate NPC smoothly
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        // Move if not at stopping distance
        if (direction.magnitude > stoppingDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            TeleportToSeat();
            NPC.transform.SetParent(Wagon.transform);

        }
    }

    private void TeleportToSeat()
    {
        isWalking = false;
        isSeated = true;
        // Teleport and sit
        transform.position = wagonSitPosition.position;
        transform.rotation = wagonSitPosition.rotation;

        // Update animations
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsSitting", true);
       // NPC.GetComponent<BoxCollider>().enabled = false;

    }
}