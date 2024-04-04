using UnityEngine;

namespace Interactables
{
    public class AnimateCube : Interactable
    {
        private Animator _animator;
        private string _startPrompt;
        void Start()
        {
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
            _animator.Play("Spin");
        }
    }
}
