using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public Rigidbody2D rb; // Player's rigid body component
    public Transform groundCheck; // Ground check object attached to player used for checking if grounded
    public LayerMask groundLayer; // The layer applied to the ground

    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 21f;
    private bool isFacingRight = true;

    private float rollSpeed;
    private State state;
    private enum State
    {
        Normal,
        Rolling
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

                animator.SetFloat("Speed", Mathf.Abs(horizontal));

                // Flips the character left or right depending on horizontal input
                if (!isFacingRight && horizontal > 0f)
                {
                    Flip();
                }
                else if (isFacingRight && horizontal < 0f)
                {
                    Flip();
                }

                animator.SetFloat("yVelocity", rb.velocity.y);

                if (IsGrounded())
                    animator.SetBool("IsJumping", false);
                if (!IsGrounded())
                    animator.SetBool("IsJumping", true);
                break;
            case State.Rolling:
                HandleRollSliding();
                break;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // When Jump button is pushed AND player is touching the ground, player's y velocity = jumpingPower and keeps horizontal velocity
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("IsJumping", true);
        }

        // When Jump button is canceled (button is released), multiply y velocity by 0.5f to allow for shorter jumps when spacebar is tapped vs held
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    // Uses the Ground Check object attached to the player and the Ground layer attached to terrain to check if they're overlapping in a 0.2f radius thus meaning the player is grounded
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1f;   // Multiplying player's x scale by -1f flips the character on the x axis
        transform.localScale = newScale;   // Set player's scale to the new, flipped scale
    }

    // When Move action is performed, grabs the Vector2's x value and passes it into the horizontal field
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed *= 2;
        }

        if (context.canceled)
        {
            speed /= 2;
        }
    }

    public void HandleRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                state = State.Rolling;
                animator.SetTrigger("Rolling");
                rollSpeed = 45f;
            }
        }
    }

    private void HandleRollSliding()
    {
        if (transform.localScale.x > 0)
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.right, rollSpeed * Time.deltaTime, groundLayer) == false)
                transform.position += new Vector3(1, 0) * rollSpeed * Time.deltaTime;
        if (transform.localScale.x < 0)
            if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.left, rollSpeed * Time.deltaTime, groundLayer) == false)
                transform.position += new Vector3(-1, 0) * rollSpeed * Time.deltaTime;

        rollSpeed -= rollSpeed * 5f * Time.deltaTime;
        if (rollSpeed < 5f)
            state = State.Normal;
    }
}
