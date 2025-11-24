using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] LayerMask Collider;
    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpForce = 10;
    [SerializeField] float TerminalVelocity = -5;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] float CoyoteDesiredTime;
    float CoyoteTime = 1;
    float Hinput;
    float preFacingDirection;
    bool jump;

    public float apexHeight = 3.5f;
    public float apexTime = .5f;
    float gravity;
    float jumpvel;
    new Vector2 velocity;
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpvel = 2 * apexHeight / apexTime;
            }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        Hinput = Input.GetAxis("Horizontal");
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
    }
   
    private void FixedUpdate()
    {
        JumpCal();
        jump = false;
        Vector2 playerInput = new Vector2(Hinput, 0);
        MovementUpdate(playerInput, MovementSpeed);
        RB.position += velocity * Time.deltaTime;
    }
    private void MovementUpdate(Vector2 playerInput, float Speed)
    {
        Vector2 Playerpos = new Vector2();
        Playerpos.x += playerInput.x * Speed;
        velocity.x = Playerpos.x;
    }
    private void JumpCal()
    {
        if (IsGrounded() && jump)
        {
            CoyoteTime = CoyoteDesiredTime;
            velocity.y = jumpvel;
        }else if (!IsGrounded())
        {
            if (velocity.y >= TerminalVelocity) {
                velocity.y += gravity * Time.deltaTime;
                velocity.y = Mathf.Max(velocity.y, -jumpvel);
            }
        }
        else 
        {
            print($"stop falling coyote timer {CoyoteTime}");
            velocity.y = 0;
        }
    }
    public bool IsWalking()
    {
        if (Hinput == 0)
        {
            return false;
        }
        preFacingDirection = Hinput;
        return true;
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (new Vector3(0, -.65f, 0)), Vector2.down, .1f,Collider);
        Debug.DrawRay(transform.position + (new Vector3(0, -.65f, 0)), Vector3.down * .1f);

        if (hit.collider != null)
        {
            CoyoteTime = 0;
            return true;
        }
        else
        {
            CoyoteTime += Time.deltaTime;
        }
        if (CoyoteTime < CoyoteDesiredTime)
        {
            return true;
        }
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        if (preFacingDirection >= 0)
        {
            return FacingDirection.right;
        }
        return FacingDirection.left;

    }
}
