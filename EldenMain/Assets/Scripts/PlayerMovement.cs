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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) == true)
        {
            animator.SetBool("IsJumping", false);
            return true;
        }
        return false;
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
}
