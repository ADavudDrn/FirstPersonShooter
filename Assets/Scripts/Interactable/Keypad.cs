using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField] private Animator Door;
    private bool _doorOpen;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    protected override void Interact()
    {
        _doorOpen = !_doorOpen;
        Door.SetBool(IsOpen, _doorOpen);
    }
}
