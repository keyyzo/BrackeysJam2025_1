using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    // Private variables

    private Vector3 inputVector;
    private Vector2 lookInputVector;

    private float xInput;
    private float yInput;
    private float zInput;
    private float mouseXInput;
    private float mouseYInput;

    private bool jumpInput;

    // Public Properties

    public Vector3 InputVector => inputVector;
    public Vector2 LookInputVector => lookInputVector;

    public bool JumpInput => jumpInput;
    
    // Cached Variables

    private InputManager inputManager;
    
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleLookInput();
        HandleJumpInput();
    }

    public void HandleMovementInput()
    {
        Vector2 movement = inputManager.GetPlayerMovement();

        xInput = movement.x;
        zInput = movement.y;

        inputVector = new Vector3(xInput, 0.0f, zInput);
    }

    void HandleLookInput()
    { 
        Vector2 look = inputManager.GetPlayerLook();
        mouseXInput = look.x;
        mouseYInput = look.y;

        lookInputVector = new Vector2(mouseXInput, mouseYInput);

    }

    void HandleJumpInput()
    { 
        jumpInput = inputManager.GetPlayerJumped();
    }

}
