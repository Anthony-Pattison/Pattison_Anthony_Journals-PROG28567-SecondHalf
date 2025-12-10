using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space(10)]
    [Header("Player Movement")]
    [SerializeField] float MovementSpeed;
    float Hinput;
    bool Dash;

    [Space(10)]
    [Header("Player Jumping")]
    public bool WallJumpCoolDown;
    [SerializeField] float TerminalVelocity = -5;
    [SerializeField] float JumpForce = 10;
    [SerializeField] float CoyoteDesiredTime;
    bool jump;

    public float apexHeight = 3.5f;
    public float apexTime = .5f;
    float CoyoteTime = 1;

    [Header("Collider")]
    [SerializeField] LayerMask Collider;
    [SerializeField] Rigidbody2D RB;

    [Space(10)]
    [Header("Dashing")]
    [SerializeField] float XDash;
    [SerializeField] float timer = 1;
    [SerializeField] TrailRenderer SP;
    [SerializeField] float DeiredDashTime = .25f;
    [SerializeField] bool DashRefreash = true;
    bool running;
    float preFacingDirection;
    float gravity;
    float jumpvel;
    new Vector2 velocity;
    public enum FacingDirection
    {
        left, right
    }
    public enum CharacterStates
    {
        Idle, Walking, Jumping, Dead
    }

    private CharacterStates state = CharacterStates.Idle;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash = true;
        }
        Hinput = Input.GetAxisRaw("Horizontal");
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
    }
    

    private void FixedUpdate()
    {
        Vector2 playerInput = new Vector2(Hinput, 0);
        JumpCal();
        WallJump(playerInput);
        if (Dash && !running && !DashRefreash) {
            StartCoroutine(AirDash(playerInput, Dash, XDash, DeiredDashTime));
        }
        jump = false;
        Dash = false;
        MovementUpdate(playerInput, MovementSpeed);
        RB.position += velocity * Time.fixedDeltaTime;
    }
    private void MovementUpdate(Vector2 playerInput, float Speed)
    {
        Vector2 Playerpos = new Vector2();
        Playerpos.x += playerInput.x * Speed;
        velocity.x = Playerpos.x;
    }
    private void WallJump(Vector2 PlayerInput)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position
        + (new Vector3(0, 0, 0)), Vector2.right * PlayerInput.x, 1f, Collider);

        Debug.DrawRay(transform.position + (new Vector3(0, 0, 0)), 
        Vector3.right * 1f * PlayerInput.x, Color.red);

        if (hit && !IsGrounded() && jump && !WallJumpCoolDown)
        {
            GetComponent<Animator>().SetBool("Fliping", true);
            WallJumpCoolDown = true;
            velocity.y = jumpvel;
            velocity.x = PlayerInput.x * -3;
        }
    }
    private IEnumerator AirDash(Vector2 PlayerInput, bool StartDash, float XDash, float timer)
    {
        running = true;
        DashRefreash = true;
        SP.enabled = true;
        XDash = PlayerInput.x* XDash;
        SP.Clear();
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            velocity.x = XDash;
            RB.position += new Vector2(velocity.x, 0) * Time.fixedDeltaTime;
            print($"running {velocity.x}");
            yield return new WaitForSeconds(.01f);
        }
        SP.enabled = false;
        running = false;
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
                velocity.y += gravity * Time.fixedDeltaTime;
                velocity.y = Mathf.Max(velocity.y, -jumpvel);
            }
        }
        else 
        {
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (new Vector3(0, -.65f, 0)), Vector2.down, .1f, Collider);
        Debug.DrawRay(transform.position + (new Vector3(0, -.65f, 0)), Vector3.down * .1f, Color.red);
        if (hit.collider != null)
        {
            GetComponent<Animator>().SetBool("Fliping", false);
            WallJumpCoolDown = false;
            DashRefreash = false;
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
