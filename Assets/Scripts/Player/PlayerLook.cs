using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        public Camera Cam;
        public float XSensitivity = 30f;
        public float YSensitivity = 30f;
        private float _xRotation = 0f;

        public void ProcessLook(Vector2 input)
        {
            float mouseX = input.x;
            float mouseY = input.y;

            _xRotation -= (mouseY * Time.deltaTime) * YSensitivity;
            _xRotation = Mathf.Clamp(_xRotation, -80, 80);
            Cam.transform.localRotation = Quaternion.Euler(_xRotation,0,0);
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * XSensitivity));
        }
    }
}