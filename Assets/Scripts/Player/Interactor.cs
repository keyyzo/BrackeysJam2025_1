using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interactor Variables")]

    [SerializeField] Transform interactorSource;
    [SerializeField] float interactorDistance;
    [SerializeField] Transform objectHoldPosition;

    bool hasObjectInHand = false;


    IInteractable currentInteractable;
    ObjectPickupDrop currentObjectPickupDrop;


    private void Update()
    {
        InteractRay();
    }

    void InteractRay()
    {
        Debug.DrawRay(interactorSource.position, interactorSource.forward * interactorDistance, Color.green);

        Ray ray = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactorDistance))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactableObject))
            {

                SetCurrentInteractable(interactableObject);
            }

            else if (hitInfo.collider.gameObject.TryGetComponent(out ObjectPickupDrop pickupableObject) && !hasObjectInHand)
            { 
                SetCurrentPickupable(pickupableObject);
                Debug.Log("Pickupable Object Found");
            }

            else
            {
                DisableCurrentInteractable();
                DisableCurrentPickupable();

            }
        }

        else
        { 
            DisableCurrentInteractable();
            DisableCurrentPickupable();
        }

    }

    //--- INTERACTABLE FUNCTIONS ---//

    void SetCurrentInteractable(IInteractable newInteractable)
    { 
        currentInteractable = newInteractable;
        currentInteractable.InteractionPrompt();
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.CancelInteractionPrompt();
            currentInteractable = null;

        }
    }

    public void ActivateInteract(bool interactTriggered)
    {
        if (interactTriggered && currentInteractable != null)
        {
            currentInteractable.Interact();
            Debug.Log("Interaction Activated");
        }

        
    }


    //--- PICKUP AND DROP FUNCTIONS ---//

    void SetCurrentPickupable(ObjectPickupDrop newPickupable)
    { 
        currentObjectPickupDrop = newPickupable;
        
    }

    void DisableCurrentPickupable()
    {
        if (currentObjectPickupDrop != null && !hasObjectInHand)
        {
            currentObjectPickupDrop = null;
        }
    }

    public void ActivatePickupObject(bool pickupTriggered)
    {
        if (pickupTriggered && currentObjectPickupDrop != null && !hasObjectInHand)
        {
            PickUpObject();
        }

        else if (pickupTriggered && currentObjectPickupDrop != null && hasObjectInHand)
        {
            DropObject();
        }
    }

    void PickUpObject()
    {
        if (currentObjectPickupDrop != null)
        {
            currentObjectPickupDrop.PickUp(objectHoldPosition);
            hasObjectInHand = true;
        }
        
    }

    void DropObject()
    {
        if (currentObjectPickupDrop != null)
        {
            currentObjectPickupDrop.Drop();
            currentObjectPickupDrop = null;
            hasObjectInHand = false;
        }
    }

    
}
