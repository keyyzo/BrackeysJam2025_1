using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Orientation Reference")]

    [SerializeField] Transform orientation;

    [Space(10)]

    [Header("Sensitivity Variables")]

    [SerializeField] float sensX = 1.0f;
    [SerializeField] float sensY = 1.0f;

    [Space(10)]

    [Header("Rotation Limits Variables")]

    [SerializeField] float lookXLimits = 90.0f;


    // Private variables

    float xRotation = 0f;
    float yRotation = 0f;

    // Cached Variables

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        HandleLook();
    }

    void HandleLook()
    { 
        float mouseX = InputManager.Instance.GetPlayerLook().x * Time.deltaTime * sensX;
        float mouseY = InputManager.Instance.GetPlayerLook().y * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -lookXLimits, lookXLimits);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);
    }

}
