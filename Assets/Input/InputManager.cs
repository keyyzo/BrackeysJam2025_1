using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    { 
        get { return _instance; }
    }

    private PlayerControls playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            _instance = this;
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool GetPlayerInteracted()
    { 
        return playerControls.Player.Interact.triggered;
    }

    public bool GetPlayerJumped()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool GetPlayerSprinting()
    {
        return playerControls.Player.Sprint.IsPressed();
    }
}
