using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public static PlayerLocomotion Instance { get; private set; }

    private const string IS_WALKING = "IsWalking";

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform followPoint;
    [SerializeField] private float rotateSpeed = 5f;
    private Vector3 moveDir { get; set; }
    public new Rigidbody rigidbody { get; private set; }
    private bool isWalking { get; set; }
    private Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        moveDir = GetCameraMovementDirection();
        rigidbody.linearVelocity = moveDir;

        isWalking = moveDir != Vector3.zero;
        animator.SetBool(IS_WALKING, isWalking);
    }

    public Transform GetFollowPoint()
    {
        return followPoint;
    }

    public Vector3 GetCameraMovementDirection()
    {
        Vector2 gameInput = GameInput.Instance.GetMovementInputNormalized();
        Vector3 inputDir = new Vector3(gameInput.x, 0f, gameInput.y);

        Vector3 cameraForward = followPoint.forward;
        Vector3 cameraRight = followPoint.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return (cameraForward * inputDir.z + cameraRight * inputDir.x) * moveSpeed;
    }
}
