using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public static PlayerLocomotion Instance { get; private set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform followPoint;
    [SerializeField] private float rotateSpeed = 5f;
    private Vector3 moveDir { get; set; }
    public new Rigidbody rigidbody { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
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

        moveDir = (cameraForward * inputDir.z + cameraRight * inputDir.x) * moveSpeed;
        rigidbody.linearVelocity = moveDir;
    }

    public Transform GetFollowPoint()
    {
        return followPoint;
    }
}
