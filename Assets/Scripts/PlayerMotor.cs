using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMotor : MonoBehaviour
{
    public float Speed = 5f;
    public float Gravity = -9.8f;
    public float JumpHeight = 3f;
    
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _isGrounded;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
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
        _controller.Move(transform.TransformDirection(moveDirection) * (Speed * Time.deltaTime));
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
}
