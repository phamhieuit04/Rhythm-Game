using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public static PlayerLocomotion Instance { get; private set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.5f;
    [SerializeField] private float maxDistance = 0.25f;
    [SerializeField] private Transform followPoint;
    [SerializeField] private float rotateSpeed = 5f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        Vector2 gameInput = GameInput.Instance.GetMovementInput().normalized;
        Vector3 inputDir = new Vector3(gameInput.x, 0f, gameInput.y);

        Vector3 cameraForward = followPoint.forward;
        Vector3 cameraRight = followPoint.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 moveDir = (cameraForward * inputDir.z + cameraRight * inputDir.x).normalized * moveSpeed;

        bool canMove = GetCanMove(moveDir);
        if (canMove)
        {
            transform.position += Time.deltaTime * moveSpeed * moveDir;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool GetCanMove(Vector3 moveDir)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, maxDistance);
    }

    public Transform GetFollowPoint()
    {
        return followPoint;
    }
}
