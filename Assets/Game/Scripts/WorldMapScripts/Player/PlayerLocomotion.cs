using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public static PlayerLocomotion Instance { get; private set; }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.5f;
    [SerializeField] private float maxDistance = 0.25f;
    private float deltaTime;

    private void Awake()
    {
        Instance = this;

        deltaTime = Time.deltaTime;
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
        Vector2 gameInput = GameInput.Instance.GetMovementInput();
        Vector3 moveDir = new Vector3(gameInput.x, 0f, gameInput.y).normalized;

        bool canMove = GetCanMove(moveDir);

        if (canMove)
        {
            transform.position += deltaTime * moveSpeed * moveDir;
        }
        else
        {
            Vector3 moveX = new Vector3(gameInput.x, 0f, 0f);
            canMove = GetCanMove(moveX);
            if (canMove)
            {
                transform.position += deltaTime * moveSpeed * moveX;
            }
            else
            {
                Vector3 moveZ = new Vector3(0f, 0f, gameInput.y);
                canMove = GetCanMove(moveZ);
                if (canMove)
                {
                    transform.position += deltaTime * moveSpeed * moveZ;
                }
            }
        }
    }

    public bool GetCanMove(Vector3 moveDir)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, maxDistance);
    }
}
