using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions inputActions;
    public event EventHandler OnChangeCameraSide;
    public event EventHandler OnZoomCamera;

    private void Awake()
    {
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.ChangeCameraSide.performed += ChangeCameraSide_Performed;
        inputActions.Player.ZoomCamera.performed += ZoomCamera_Performed;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void ChangeCameraSide_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnChangeCameraSide?.Invoke(this, EventArgs.Empty);
    }

    private void ZoomCamera_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnZoomCamera?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementInput()
    {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseInput()
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetMouseScrollInput()
    {
        return inputActions.Player.ZoomCamera.ReadValue<Vector2>();
    }
}
