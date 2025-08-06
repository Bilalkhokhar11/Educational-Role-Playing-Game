using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    [Header("Ground Check")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    [Header("Raft")]
    [SerializeField] Transform oarSocket;
    [SerializeField] GameObject oarModel;
    [SerializeField] float maxPaddleSpeed = 1.5f;

    // Components
    private CharacterController characterController;
    private Animator animator;
    private Camera cameraMain;

    // Movement
    private float ySpeed;
    private bool isGrounded;
    private Quaternion targetRotation;

    // Raft
    private RaftController currentRaft;
    private bool isOnRaft;
    private float currentPaddleIntensity;
    public AudioSource audioSource;
    public AudioClip footstepClip;
    public float moveThreshold = 0.1f;

    private void Start()
    {
    }
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraMain = Camera.main;
        if (oarModel) oarModel.SetActive(false);
        
    }

    void Update()
    {
        

            
        
        HandleGroundCheck();

        if (isOnRaft)
        {
            HandleRaftInput();
            UpdateRaftAnimation();
        }
        else
        {
            HandleGroundMovement();

        }
    }

    void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundLayer);
    }

    void HandleGroundMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(h, 0, v).normalized;

        // Align movement to camera's forward direction (ignoring pitch)
        Vector3 camForward = cameraMain.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cameraMain.transform.right;
        Vector3 moveDir = (camRight * moveInput.x + camForward * moveInput.z).normalized;

        // Gravity
        ySpeed = isGrounded ? -0.5f : ySpeed + Physics.gravity.y * Time.deltaTime;

        // Movement
        Vector3 velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        // Rotation towards movement direction if there is input
        if (moveInput.magnitude > 0.1f)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Update locomotion animation blend tree
        animator.SetFloat("moveAmount", moveInput.magnitude, 0.2f, Time.deltaTime);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = footstepClip;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void HandleRaftInput()
    {
       

        // Get input for paddling intensity (vertical input primarily, add horizontal if needed)
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        currentPaddleIntensity = Mathf.Clamp01(Mathf.Abs(v) + Mathf.Abs(h) * 0.5f);
    }

    void UpdateRaftAnimation()
    {
        animator.SetBool("IsPaddling", currentPaddleIntensity > 0.1f);
        // Map paddle intensity to a speed multiplier range (adjust as needed)
        animator.SetFloat("PaddlingSpeed", Mathf.Lerp(0.5f, maxPaddleSpeed, currentPaddleIntensity));
    }

    public void EnterRaft(RaftController Raft)
    {
        // Disable ground physics and checks
        isGrounded = true;
        ySpeed = 0;
        characterController.enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }

        // Parent the player to the raft so that the player moves with it
        transform.SetParent(Raft.transform, true);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isOnRaft = true;
        currentRaft = Raft;

        animator.SetBool("IsPaddling", true);
        if (oarModel != null) oarModel.SetActive(true);

        Debug.Log("Entered Raft");
    }

  

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }
}
