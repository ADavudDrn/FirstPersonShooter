using System;
using Photon.Pun;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool UseEvents;
    public string PromptMessage;
    
    protected PhotonView _photonView;

    protected virtual void Start()
    {
        _photonView = transform.GetComponent<PhotonView>();
    }

    public void BaseInteract()
    {
        if (_photonView == null)
        {
            if(UseEvents)
                EventInteract();
            
            return;
        }
        
        if(UseEvents)
            _photonView.RPC ("EventInteract", RpcTarget.AllBufferedViaServer);
       
        Interact();
    }
    
    protected virtual void Interact()
    {
        
    }

    [PunRPC]
    private void EventInteract()
    {
        GetComponent<InteractionEvent>().OnInteract?.Invoke();
    }
}
