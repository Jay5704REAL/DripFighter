using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Animator anim;
    Vector2 moveInput;
    bool facingRight = true;

    bool IsGrounded()
    {
        if (groundCheck == null) return true;
        return Physics2D.OverlapCircle(groundCheck.position, 0.12f, groundLayer);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Called by Input System (PlayerInput SendMessages) => OnMove
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            anim.SetTrigger("Attack");
            var cm = GetComponent<ComboManager>();
            if (cm) cm.RegisterAttackInput();
        }
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(moveInput.x));
        if (moveInput.x > 0.01f && !facingRight) Flip();
        else if (moveInput.x < -0.01f && facingRight) Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }
}