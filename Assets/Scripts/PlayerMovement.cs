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


    // Private variables

    float currentSpeed = 0.0f;

    bool isSprinting = false;

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

        // TODO: May need to implement a way to force player to the ground if not done outside of this function
    }
}
