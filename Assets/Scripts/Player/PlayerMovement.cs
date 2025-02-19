using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Player Speed Variables")]

    [SerializeField] float baseMaxSpeed = 5.0f;
    [SerializeField] float baseAccelerationSpeed = 1.0f;
    [SerializeField] float baseDecelerationSpeed = 2.0f;

    [Space(10)]

    [Header("Sprint Player Speed Variables")]

    [SerializeField] float sprintMaxSpeed = 10.0f;
    [SerializeField] float sprintAccelerationSpeed = 1.5f;

    [Space(10)]

    [Header("Player Jump Variables")]

    [SerializeField] float jumpStrength = 5.0f;
    [SerializeField] float jumpMultiplier = -2.0f;

    [Space(10)]

    [Header("Ground Check Variables")]

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [Space(10)]

    [Header("Gravity Variables")]

    [SerializeField] float gravityStrength = -9.81f;
    [SerializeField] float gravityMultiplier = 2.0f;


    // Private variables

    Vector3 velocity;

    float currentSpeed = 0.0f;

    bool isSprinting = false;
    bool isGrounded;

    // Cached Variables

    private CharacterController characterController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        GroundedReset();

        ApplyGravity();
    }

    public void MovePlayer(Vector3 inputVec)
    {
        if (inputVec == Vector3.zero)
        {
            if (currentSpeed > 0f)
            {
                currentSpeed -= baseDecelerationSpeed * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);
            }
        }

        else
        {
            if (isSprinting)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, sprintMaxSpeed, Time.deltaTime * sprintAccelerationSpeed);
            }

            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, baseMaxSpeed, Time.deltaTime * baseAccelerationSpeed);
            }
            
        }

        Vector3 movementDirection = transform.right * inputVec.x + transform.forward * inputVec.z;

        Vector3 movement = movementDirection.normalized * currentSpeed * Time.deltaTime;

        characterController.Move(movement);

        
    }

    /// <summary>
    /// Activates the player jump mechanic, by passing in a variable from input to determine when to jump
    /// </summary>
    public void JumpPlayer(bool jumpTriggered)
    {
        if (jumpTriggered && isGrounded)
        {
            isGrounded = false;

            velocity.y = Mathf.Sqrt(jumpStrength * jumpMultiplier);
        }
    }

    /// <summary>
    /// Activates the player sprint mechanic, by passing in a variable from input to determine when to sprint
    /// </summary>
    public void PlayerSprint(bool sprintTriggered)
    {
        if (sprintTriggered && !isSprinting)
        {
            isSprinting = true;
            Debug.Log("Sprinting");
        }

        else if (!sprintTriggered)
        {
            isSprinting = false;
            Debug.Log("Walking");
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }

    /// <summary>
    /// Applies gravity to the player, to ensure whenever not jumping the player is on the ground
    /// </summary>
    void ApplyGravity()
    {
        

        if (!isGrounded && velocity.y < 0f)
        {
            velocity.y += gravityStrength * gravityMultiplier * Time.deltaTime;

            //Debug.Log("Stronger Gravity");
        }

        else
        {
            velocity.y += gravityStrength * Time.deltaTime;
            //Debug.Log("Normal Gravity");
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Checks to see if the player is touching the ground with a negative velocity,
    /// if so sets the velocity to a specified value to avoid possible bugs
    /// </summary>
    void GroundedReset()
    {
        if (GroundedCheck() && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    /// <summary>
    /// Returns a value based on whether the player is touching the ground or not
    /// </summary>
    bool GroundedCheck()
    {
        if (Physics.CheckSphere(groundCheck.position, groundDistance))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
