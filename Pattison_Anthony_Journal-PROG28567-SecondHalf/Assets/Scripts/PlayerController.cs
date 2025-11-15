using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpForce = 10;
    [SerializeField] Rigidbody2D RB;
    float Hinput;
    float preFacingDirection;
    [SerializeField] bool jump;
    [SerializeField] float GroundCheck;
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        jump = false;
        Hinput = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }

        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

    }
    private void FixedUpdate()
    {

        Vector2 playerInput = new Vector2(Hinput, 0);
        MovementUpdate(playerInput, MovementSpeed, jump);
    }
    private void MovementUpdate(Vector2 playerInput, float Speed, bool Jump)
    {
        Vector2 Playerpos = RB.position;
        Playerpos += playerInput * Speed * Time.deltaTime;
        RB.position = Playerpos;
        if (!Jump || !IsGrounded())
        {
            return;
        }

        RB.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (new Vector3(0, -.65f, 0)), Vector2.down, .5f);
        Debug.DrawRay(transform.position + (new Vector3(0, -.65f, 0)), Vector3.down * .5f);
        if (hit.collider  != null && hit.collider.tag != "Player")
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
