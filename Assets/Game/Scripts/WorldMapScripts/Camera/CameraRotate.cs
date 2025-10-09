using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public static CameraRotate Instance { get; private set; }

    [SerializeField] private float rotateSpeed = 150f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleRotate();
    }

    public void HandleRotate()
    {
        Vector2 mouseDelta = GameInput.Instance.GetMouseInput();
        mouseDelta *= rotateSpeed * Time.deltaTime;

        rotationY += mouseDelta.x;
        rotationX -= mouseDelta.y;
        rotationX = Mathf.Clamp(rotationX, -10f, 40f);

        Transform followPoint = PlayerLocomotion.Instance.GetFollowPoint();
        followPoint.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
