using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveForce;

    [SerializeField]
    float jumpForce;

    [SerializeField]
    float gravity;

    [SerializeField]
    float terminalVelocity;

    [SerializeField]
    Transform ground;

    [SerializeField]
    LayerMask groundLayer;


    bool isCanJump;
    bool isPressedJump;

    Vector2 inputVector;
    Vector2 velocity;
    Vector2 rayDirection;

    RaycastHit2D hit;
    Rigidbody2D rigid;


    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        InputHandler();
    }

    void FixedUpdate()
    {
        CheckGround();
        MovementHandler();
    }

    void Initialize()
    {
        isPressedJump = false;
        isCanJump = false;
        rayDirection = new Vector2(1.0f, -1.0f);
        rigid = GetComponent<Rigidbody2D>();
    }

    void InputHandler()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        isPressedJump = Input.GetButtonDown("Jump");
    }

    void CheckGround()
    {
        hit = Physics2D.Raycast(ground.position, rayDirection, 0.2f, groundLayer);
        isCanJump = (hit.collider != null);
    }

    void MovementHandler()
    {
        velocity.x = (moveForce * inputVector.x);

        if (isCanJump && isPressedJump) {
            velocity.y = jumpForce;
        }
        else {
            velocity.y -= gravity;
            velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, jumpForce);
        }

        rigid.velocity = (velocity * Time.deltaTime);
    }
}
