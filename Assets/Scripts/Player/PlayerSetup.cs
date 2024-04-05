using UnityEngine;

namespace Player
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private InputManager InputManager;
        [SerializeField] private PlayerMotor Motor;
        [SerializeField] private PlayerLook Look;
        [SerializeField] private PlayerInteract Interact;
        [SerializeField] private PlayerUI UI;
        [SerializeField] private GameObject Camera;
        [SerializeField] private GameObject Canvas;

        public void IsLocalPlayer()
        {
            InputManager.enabled = true;
            Motor.enabled = true;
            Look.enabled = true;
            Interact.enabled = true;
            UI.enabled = true;
            Camera.SetActive(true);
            Canvas.SetActive(true);
        }
    }
}
