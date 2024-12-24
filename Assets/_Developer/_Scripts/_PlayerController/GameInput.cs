using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    public event EventHandler OnInterect;
    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Player.Enable();

        
    }

    private void OnEnable()
    {
        _playerInputAction.Player.Interect.performed += Interect;
    }

    private void Interect(InputAction.CallbackContext obj)
    {
        OnInterect?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 InputVectorNormalized()
    {
        Vector2 inputVector = _playerInputAction.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    } 
}
