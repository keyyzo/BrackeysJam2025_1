using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    [Header("Headbob Variables")]

    [SerializeField] GameObject headPivotObject;
    [SerializeField] float bobAmount = 0.5f;
    [SerializeField] float bobSpeed = 2.0f;


   
    float bobTimer = 0f;

    public void CalculateHeadbob(float currentMovementSpeed)
    {
        Vector3 movementInput = new Vector3(InputManager.Instance.GetPlayerMovement().x, 0f, InputManager.Instance.GetPlayerMovement().y);

        if (movementInput.magnitude > 0f)
        {
            bobTimer += Time.deltaTime * bobSpeed * currentMovementSpeed;
            
        }


        float bobOffset = (Mathf.Sin(bobTimer) * bobAmount) + (Mathf.Cos(bobTimer) * bobAmount);

        headPivotObject.transform.localPosition = new Vector3(headPivotObject.transform.localPosition.x, bobOffset, headPivotObject.transform.localPosition.z);

    }
}
