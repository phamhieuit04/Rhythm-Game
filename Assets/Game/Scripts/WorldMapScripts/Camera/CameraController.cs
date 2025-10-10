using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private float rotateSpeed = 150f;
    [SerializeField] private float minRotateRange = -10f;
    [SerializeField] private float maxRotateRange = 40f;
    [SerializeField] private float changeCameraSideSpeed = 5f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float zoomLerpSpeed = 6f;
    [SerializeField] private float minZoomRange = 1.15f;
    [SerializeField] private float maxZoomRange = 5f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private CinemachineThirdPersonFollow thirdPersonCamera;
    private bool isChangeCameraSide = false;
    private Vector2 mouseScrollInput;
    private float targetZoom;

    void Start()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        thirdPersonCamera = GetComponent<CinemachineThirdPersonFollow>();
        targetZoom = thirdPersonCamera.CameraDistance;

        GameInput.Instance.OnChangeCameraSide += GameInput_OnChangeCameraSide;
        GameInput.Instance.OnZoomCamera += GameInput_OnZoomCamera;
    }

    void Update()
    {
        HandleRotate();
        HandleZoom();
        HandleChangeCameraSide();
    }

    public void HandleRotate()
    {
        Vector2 mouseDelta = GameInput.Instance.GetMouseInput();
        mouseDelta *= rotateSpeed * Time.deltaTime;

        rotationY += mouseDelta.x;
        rotationX -= mouseDelta.y;
        rotationX = Mathf.Clamp(rotationX, minRotateRange, maxRotateRange);

        Transform followPoint = PlayerLocomotion.Instance.GetFollowPoint();
        followPoint.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    public void HandleZoom()
    {
        if (mouseScrollInput.y != 0f)
        {
            targetZoom = Mathf.Clamp(thirdPersonCamera.CameraDistance - mouseScrollInput.y * zoomSpeed, minZoomRange, maxZoomRange);
            mouseScrollInput = Vector2.zero;
        }
        float currentZoom = thirdPersonCamera.CameraDistance;
        thirdPersonCamera.CameraDistance = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }

    public void HandleChangeCameraSide()
    {
        float currentSide = thirdPersonCamera.CameraSide;
        float targetSide = isChangeCameraSide ? 0.5f : 1f;
        thirdPersonCamera.CameraSide = thirdPersonCamera.CameraSide = Mathf.Lerp(currentSide, targetSide, Time.deltaTime * changeCameraSideSpeed);
    }

    private void GameInput_OnChangeCameraSide(object sender, System.EventArgs e)
    {
        isChangeCameraSide = !isChangeCameraSide;
    }

    private void GameInput_OnZoomCamera(object sender, System.EventArgs e)
    {
        mouseScrollInput = GameInput.Instance.GetMouseScrollInput();
    }
}
