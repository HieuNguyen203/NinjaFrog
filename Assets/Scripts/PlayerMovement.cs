using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { idle, running, jumping, falling, air_jump, sliding};
    private MovementState state;
    
    private Rigidbody2D rb;
    private Animator ani;
    private SpriteRenderer sprite_render;
    private BoxCollider2D box_collider;

    public bool can_dash = false;
    public bool can_double_jump = false;
    public bool can_wall_jump = false;

    [Header("Jump")]
    [SerializeField] private LayerMask jumpable_ground;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private AudioSource JumpSound;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimer;
    private bool air_jump = true;


    [Header("Dash")]
    [SerializeField] private AudioSource DashSound;
    [SerializeField] private float dashDuration = 0.5f; // Adjust as needed
    [SerializeField] private float dashSpeed = 10f; // Adjust as needed
    [SerializeField] private DashAfterimage afterimage;
    private bool isDashing = false;

    [Header("Slide")]
    [SerializeField] private float slide_speed = 0;
    private bool isSliding = false;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sprite_render = GetComponent<SpriteRenderer>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Dash") && !isDashing && can_dash)
        {
            StartCoroutine(Dash(dirX));
        }
        else
        {

            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (IsGrounded() || isSliding)
            {
                coyoteTimer = coyoteTime;
                air_jump = false;
            }
            else
            {
                coyoteTimer -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (coyoteTimer > 0.1f)
                {
                    rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
                    coyoteTimer = 0f;
                    JumpSound.Play();
                }
                else if (!air_jump && can_double_jump)
                {
                    air_jump = true;
                    rb.velocity = new Vector2(dirX * moveSpeed, jumpForce);
                    JumpSound.Play();
                }
            }

            if(IsHugWall() && !IsGrounded() && rb.velocity.y < 0.1 && can_wall_jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, slide_speed);
                isSliding = true;
            }
            else
            {
                isSliding = false;
            }


        }
        UpdateAnimation(dirX);
    }

    private void UpdateAnimation(float dirX)
    {
        if (dirX == 0)
        {
            state = MovementState.idle;
        }
        else
        {
            state = MovementState.running;
            sprite_render.flipX = (dirX < 0); 
        }

        if (rb.velocity.y > 0.1f)
        {
            if (!air_jump)
                state = MovementState.jumping;
            else
                state = MovementState.air_jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            if(!isSliding)
                state = MovementState.falling;
            else
                state = MovementState.sliding;
        }

        // Update the animator based on the state
        ani.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box_collider.bounds.center, box_collider.bounds.size, 0f, Vector2.down, .1f, jumpable_ground);
    }

    private bool IsHugWall()
    {
        return Physics2D.BoxCast(box_collider.bounds.center, box_collider.bounds.size, 0f, sprite_render.flipX ? Vector2.left : Vector2.right, .1f, jumpable_ground);
    }

    private IEnumerator Dash(float dirX)
    {
        isDashing = true;

        DashSound.Play();

        if(dirX== 0)
        {
            dirX = sprite_render.flipX ? 1 : -1;
        }
        
        ActivateAfterimage();
        
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            rb.velocity = new Vector2(dirX * dashSpeed, 0f);
            yield return null;
        }

        DeactivateAfterimage();

        yield return new WaitForSeconds(1f);

        isDashing = false;
    }

    public void ActivateAfterimage()
    {
        afterimage.create_afterimage = true;
    }

    public void DeactivateAfterimage()
    {
        afterimage.create_afterimage = false;
    }
}
