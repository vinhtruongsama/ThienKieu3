using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public sealed class Map001PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.8f;
    [SerializeField] private float jumpImpulse = 11.5f;
    [SerializeField] private float stunnedSpeed = 2.0f;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    private float stunUntil;

    public float Health { get; private set; } = 100f;
    public bool HasMoved { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        body.freezeRotation = true;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        float horizontal = 0f;
        if (keyboard != null)
        {
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) horizontal -= 1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) horizontal += 1f;
            if (keyboard.spaceKey.wasPressedThisFrame && grounded && Time.time >= stunUntil)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpImpulse);
                grounded = false;
                animator?.SetTrigger("Jump");
            }
        }

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
        grounded = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.55f)
            {
                grounded = true;
                break;
            }
        }
    }

    public void TakeHit(Vector2 from)
    {
        Health = Mathf.Max(1f, Health - 8f);
        stunUntil = Time.time + 0.55f;
        body.linearVelocity = new Vector2(Mathf.Sign(transform.position.x - from.x) * 4.5f, 3.5f);
        animator?.SetTrigger("Hurt");
    }
}
