using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        public float BaseSpeed = 5f;
        public float Gravity = -9.8f;
        public float JumpHeight = 3f;

        [SerializeField] private float CrouchDuration = .5f;
        private CharacterController _controller;
        private Vector3 _playerVelocity;
        private bool _isGrounded;
    
        private bool _crouching;
        private bool _walking;
        private float _speed;

        private Tween _crouchTween;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _speed = BaseSpeed;
        }

        void Update()
        {
            _isGrounded = _controller.isGrounded;
        }

        public void ProcessMove(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            _controller.Move(transform.TransformDirection(moveDirection) * (_speed * Time.deltaTime));
            _playerVelocity.y += Gravity * Time.deltaTime;
            if (_isGrounded && _playerVelocity.y < 0)
                _playerVelocity.y = -2;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        public void Jump()
        {
            if(!_isGrounded)
                return;

            _playerVelocity.y = Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        }

        public void Crouch()
        {
            _walking = false;
            _crouching = true;
            var start = _controller.height;
            var time = (_controller.height - 1) * CrouchDuration;
            _speed = BaseSpeed * .3f;
            _crouchTween?.Kill();
            _crouchTween = DOVirtual.Float(start, 1, time, val => { _controller.height = val; });
        }
        public void UnCrouch()
        {
            if(_walking)
                return;

            _crouching = false;
            var start = _controller.height;
            var time = (2 - _controller.height) * CrouchDuration;
            _speed = BaseSpeed;
            _crouchTween?.Kill();
            _crouchTween = DOVirtual.Float(start, 2, time, val => { _controller.height = val; });
        }
    
        public void CrouchForGamepad()
        {
            _crouching = !_crouching;
            if (_crouching)
            {
                NormalWalk();
                Crouch();
            }
            else
                UnCrouch();
        }
    
        public void SlowWalk()
        {
            if(_crouching)
                return;

            _walking = true;
            _speed *= .5f;
        }
    
        public void NormalWalk()
        {
            if(_crouching)
                return;

            _walking = false;
            _speed = BaseSpeed;
        }
    
        public void WalkForGamepad()
        {
            _walking = !_walking;
            if (_walking)
            {
                UnCrouch();
                SlowWalk();
            }
            else
                NormalWalk();
        }
    }
}
