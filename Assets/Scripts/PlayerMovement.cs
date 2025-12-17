using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Dash")]
    public float dashDistance = 3f;   //Change dash length
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir = Vector2.right;

    private bool isDashing;
    private float dashCooldownTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        HandleDashCooldown();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        rb.linearVelocity = moveInput * moveSpeed;
    }

    void HandleInput()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;
        if (Input.GetKey(KeyCode.W)) vertical = 1f;

        moveInput = new Vector2(horizontal, vertical).normalized;

        if (moveInput != Vector2.zero)
            lastMoveDir = moveInput;

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + lastMoveDir * dashDistance;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPos);
        isDashing = false;
    }

    void HandleDashCooldown()
    {
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;
    }
}

