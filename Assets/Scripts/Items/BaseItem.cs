using UnityEngine;

public abstract class BaseItem : MonoBehaviour, IInteractable
{
    public abstract void CancelInteractionPrompt();
    public abstract void Interact();
    public abstract void InteractionPrompt();
}
