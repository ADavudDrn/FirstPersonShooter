using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float Distance = 3;
        [SerializeField] private LayerMask Mask;
    
        private Camera _cam;

        private PlayerUI _playerUI;

        private InputManager _inputManager;
        void Start()
        {
            _cam = GetComponent<PlayerLook>().Cam;
            _playerUI = GetComponent<PlayerUI>();
            _inputManager = GetComponent<InputManager>();
        }

        void Update()
        {
            _playerUI.UpdateText(string.Empty);
            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * Distance);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Distance, Mask))
            {
                if (hitInfo.collider.TryGetComponent(out Interactable interactable))
                {
                    _playerUI.UpdateText(interactable.PromptMessage);
                    if (_inputManager.OnFoot.Interact.triggered)
                    {
                        interactable.BaseInteract();
                    }
                }
            }
        }
    }
}
