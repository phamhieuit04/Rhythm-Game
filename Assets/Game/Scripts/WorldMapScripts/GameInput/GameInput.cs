using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public Vector2 GetMovementInput()
    {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseInput()
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }
}
