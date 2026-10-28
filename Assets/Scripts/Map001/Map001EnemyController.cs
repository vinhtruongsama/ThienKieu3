using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Map001EnemyController : MonoBehaviour
{
    public Map001PlayerController player;
    public float runSpeed = 2.35f;
    public float leapDistance = 8f;
    private Rigidbody2D body;
    private Animator animator;
    private bool frozen;
    private float nextAction;
    private float nextHit;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        body.freezeRotation = true;
    }

    private void Start() { animator?.SetTrigger("Attack"); }

    private void Update()
    {
        if (player == null || frozen) { body.linearVelocity = new Vector2(0f, body.linearVelocity.y); return; }
        float dx = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(dx) > 0.8f)
        {
            float direction = Mathf.Sign(dx);
            body.linearVelocity = new Vector2(direction * runSpeed, body.linearVelocity.y);
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;
        }
        if (Mathf.Abs(dx) < leapDistance && Time.time >= nextAction)
        {
            nextAction = Time.time + 3.2f;
            animator?.SetTrigger("Jump");
            body.linearVelocity = new Vector2(Mathf.Sign(dx) * runSpeed * 1.8f, 8f);
        }
        if (Mathf.Abs(dx) < 1.25f && Time.time >= nextHit)
        {
            nextHit = Time.time + 0.85f;
            player.TakeHit(transform.position);
        }
    }

    public void Freeze()
    {
        frozen = true;
        body.linearVelocity = Vector2.zero;
        animator?.SetTrigger("Hurt");
    }
}
