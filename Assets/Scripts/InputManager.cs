using DefaultNamespace;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    public PlayerInput.OnFootActions OnFoot;

    private PlayerMotor _motor;
    private PlayerLook _look;
    void Awake()
    {
        _playerInput = new PlayerInput();
        OnFoot = _playerInput.OnFoot;
        _motor = GetComponent<PlayerMotor>();
        _look = GetComponent<PlayerLook>();
        OnFoot.Jump.performed += _ => _motor.Jump();
        OnFoot.Crouch.started += _ => _motor.Crouch();
        OnFoot.Crouch.canceled += _ => _motor.UnCrouch();
        OnFoot.Walk.started += _ => _motor.SlowWalk();
        OnFoot.Walk.canceled += _ => _motor.NormalWalk();
        OnFoot.Sprint.started += _ => _motor.Sprint();
        OnFoot.Sprint.canceled += _ => _motor.NormalWalk();
        OnFoot.CrouchGamepad.performed += _ => _motor.CrouchForGamepad();
        OnFoot.WalkGamepad.performed += _ => _motor.WalkForGamepad();
        OnFoot.SprintGamepad.performed += _ => _motor.SprintForGamepad();

    }

    private void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }

    void FixedUpdate()
    {
        _motor.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _look.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
    }
}
