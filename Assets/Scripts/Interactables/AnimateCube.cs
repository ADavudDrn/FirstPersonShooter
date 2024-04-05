using Photon.Pun;
using UnityEngine;

namespace Interactables
{
    public class AnimateCube : Interactable
    {
        private Animator _animator;
        private string _startPrompt;
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _startPrompt = PromptMessage;
        }

        void Update()
        {

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
                PromptMessage = _startPrompt;
            else
                PromptMessage = "Animating...";
        }

        
        protected override void Interact()
        {
            _photonView.RPC ("Animate", RpcTarget.AllBufferedViaServer);
        }
        [PunRPC]
        private void Animate()
        {
            _animator.Play("Spin");
        }
    }
}
