using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerAudio))]
public class PlayerController : MonoBehaviour
{





    // Cached Variables
    
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerAudio playerAudio;
    Interactor interactor;
    PlayerHeadBob headBob;

    private void Awake()
    {
        InitialisePlayerComponents();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.JumpPlayer(playerInput.JumpInput);
        playerMovement.PlayerSprint(playerInput.SprintInput);
        playerMovement.MovePlayer(playerInput.InputVector);
        interactor.ActivateInteract(playerInput.InteractInput);
        interactor.ActivatePickupObject(playerInput.InteractInput);
        headBob.CalculateHeadbob(playerMovement.CurrentSpeed);
        
    }

    void InitialisePlayerComponents()
    { 
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAudio = GetComponent<PlayerAudio>();
        interactor = GetComponent<Interactor>();
        headBob = GetComponent<PlayerHeadBob>();
        
    }
}
