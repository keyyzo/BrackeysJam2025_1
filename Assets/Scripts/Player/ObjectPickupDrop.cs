using UnityEngine;

public class ObjectPickupDrop : MonoBehaviour
{
    

    Rigidbody rb;
    Transform heldPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (heldPosition != null)
        {
            float lerpSpeed = 10.0f;

            Vector3 newPosition = Vector3.Lerp(transform.position, heldPosition.position, Time.deltaTime * lerpSpeed);

            rb.MovePosition(newPosition);
        }
    }

    public void PickUp(Transform positionToHold)
    { 
        heldPosition = positionToHold;
        rb.useGravity = false;


    }

    public void Drop()
    {
        heldPosition = null;
        rb.useGravity = true;
    }
}
