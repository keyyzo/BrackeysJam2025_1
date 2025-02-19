using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interactor Variables")]

    [SerializeField] Transform interactorSource;
    [SerializeField] float interactorDistance;

    IInteractable currentInteractable;



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
                Debug.Log("An interactable object is within reach");

                SetCurrentInteractable(interactableObject);
            }

            else
            {
                DisableCurrentInteractable();
                Debug.Log("No interactable object within reach");
            }
        }

        else
        { 
            DisableCurrentInteractable();
            Debug.Log("No interactable object within reach");
        }

    }

    void SetCurrentInteractable(IInteractable newInteractable)
    { 
        currentInteractable = newInteractable;
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
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
}
