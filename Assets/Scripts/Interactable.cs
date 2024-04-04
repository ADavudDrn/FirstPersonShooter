using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool UseEvents;
    public string PromptMessage;

    public void BaseInteract()
    {
        if(UseEvents)
            GetComponent<InteractionEvent>().OnInteract?.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        
    }
}
