using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public static CameraRotate Instance { get; private set; }

    [SerializeField] private float rotateSpeed = 150f;
    [SerializeField] private float minRange = -10f;
    [SerializeField] private float maxRange = 40f;
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
        rotationX = Mathf.Clamp(rotationX, minRange, maxRange);

        Transform followPoint = PlayerLocomotion.Instance.GetFollowPoint();
        followPoint.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
