using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RaftController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotationSpeed = 150f;

    // Reference to the player on board (if any)
    private PlayerController playerController;
    private Rigidbody rb;
    private bool hasPlayer;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ConfigureRigidbody();
    }

    void ConfigureRigidbody()
    {
        // Configure physics properties to match CharacterController behavior
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        rb.mass = 1000f; // Heavy mass for stability
        rb.drag = 5f; // Matches CharacterController's immediate stop
        rb.angularDrag = 10f; // Prevents over-rotation
        rb.useGravity = true;   
    }

    void FixedUpdate()
    {
        if (!hasPlayer) return;

        // Get input axes
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Movement (matches CharacterController's immediate movement)
        Vector3 move = transform.forward * verticalInput * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // Rotation (matches Transform.Rotate behavior)
        float rotationAmount = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f));
    }

    void Update()
    {
        // Maintain player's position (same as original)
        if (hasPlayer && playerController != null)
        {
            playerController.transform.localPosition = Vector3.zero;
            playerController.transform.localRotation = Quaternion.identity;
        }
    }

    public void SetPlayerOnBoard(bool state, PlayerController pc)
    {
        hasPlayer = state;
        playerController = pc;
    }
}