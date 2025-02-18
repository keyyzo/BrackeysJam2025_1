using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(PlayerAudio))]
public class PlayerController : MonoBehaviour
{





    // Cached Variables
    
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerAudio playerAudio;


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
        playerMovement.MovePlayer(playerInput.InputVector);
        playerMovement.JumpPlayer(playerInput.JumpInput);
    }

    void InitialisePlayerComponents()
    { 
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAudio = GetComponent<PlayerAudio>();

        
    }
}
