using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public sealed class Map001PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.8f;
    [SerializeField] private float jumpImpulse = 11.5f;
    [SerializeField, Min(1)] private int maxJumps = 2;
    [SerializeField] private float stunnedSpeed = 2.0f;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    private int jumpsRemaining;
    private float stunUntil;
    private readonly HashSet<Collider2D> groundContacts = new HashSet<Collider2D>();
#if UNITY_EDITOR
    private bool editorTestAutopilot;
    private bool editorTestJumped;
    private bool editorTestJumpScreenshotTaken;
    public bool IsEditorTestAutopilot => editorTestAutopilot;
#endif

    public float Health { get; private set; } = 100f;
    public bool HasMoved { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsRemaining = Mathf.Max(1, maxJumps);
        body.freezeRotation = true;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void OnValidate()
    {
        maxJumps = Mathf.Max(1, maxJumps);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        float horizontal = 0f;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontal -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontal += 1f;
            if ((keyboard.spaceKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
            && jumpsRemaining > 0 && Time.time >= stunUntil)
            {
                Jump();
            }
        }
#if UNITY_EDITOR
        if (editorTestAutopilot)
        {
            horizontal = 1f;
            if (grounded && Time.time >= stunUntil && ShouldEditorTestJump())
            {
                editorTestJumped = true;
                Jump();
            }
        }
#endif

        if (Time.time < stunUntil) horizontal *= 0.35f;
        if (Mathf.Abs(horizontal) > 0.01f) HasMoved = true;
        body.linearVelocity = new Vector2(horizontal * (Time.time < stunUntil ? stunnedSpeed : moveSpeed), body.linearVelocity.y);
        if (horizontal != 0f)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(horizontal);
            transform.localScale = scale;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            if (Time.time < stunUntil && animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") == false)
                animator.SetTrigger("Hurt");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.55f && body.linearVelocity.y <= 0.01f)
            {
                grounded = true;
                jumpsRemaining = Mathf.Max(1, maxJumps);
                groundContacts.Add(collision.collider);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundContacts.Remove(collision.collider);
        grounded = groundContacts.Count > 0;
    }

    private void Jump()
    {
        if (jumpsRemaining <= 0) return;
        jumpsRemaining--;
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpImpulse);
        grounded = false;
        groundContacts.Clear();
        animator?.SetTrigger("Jump");
        Debug.Log("MAP-001: Người chơi nhảy.");
#if UNITY_EDITOR
        if (editorTestAutopilot && !editorTestJumpScreenshotTaken)
        {
            editorTestJumpScreenshotTaken = true;
            MapTestScreenshot.Capture(this, "20261102-MAP001-CuNhayQuai.png");
        }
#endif
    }

#if UNITY_EDITOR
    private bool ShouldEditorTestJump()
    {
        if (!editorTestJumped && transform.position.x >= 6f) return true;
        if (!editorTestJumped) return false;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null) return false;
        Bounds bounds = collider.bounds;
        Vector2 frontFoot = new Vector2(bounds.max.x + 0.18f, bounds.min.y + 0.06f);
        return Physics2D.Raycast(frontFoot, Vector2.down, 0.45f).collider == null;
    }
#endif

#if UNITY_EDITOR
    public void EnableEditorTestAutopilotForIntro()
    {
        editorTestAutopilot = true;
        MapTestScreenshot.Capture(this, "20261102-MAP001-BatDau.png");
    }
#endif

    public void TakeHit(Vector2 from)
    {
        Health = Mathf.Max(1f, Health - 8f);
        stunUntil = Time.time + 0.55f;
        body.linearVelocity = new Vector2(Mathf.Sign(transform.position.x - from.x) * 4.5f, 3.5f);
        animator?.SetTrigger("Hurt");
    }
}
