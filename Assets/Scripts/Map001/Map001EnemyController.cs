using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public sealed class Map001EnemyController : MonoBehaviour
{
    private enum BehaviorState
    {
        Waiting,
        Appearing,
        Growling,
        Chasing,
        Reordering,
        Pouncing,
        Recovering,
        Frozen
    }

    private const float PounceFlightDuration = 0.48f;
    private const float PounceJumpImpulse = 8.4f;
    private const float PounceMaxSpeed = 12.5f;
    private const float ReorderJumpImpulse = 13f;
    private const float MinimumStandingGap = 4.0f;

    public Map001PlayerController player;
    public float runSpeed = 4.0f;
    public float leapDistance = 6f;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer sprite;
    private AudioSource growlSource;
    private Map001EnemyController[] group;
    private Map001RuntimeDirector director;
    private readonly HashSet<Collider2D> groundContacts = new HashSet<Collider2D>();
    private Vector3 baseScale;
    private Color baseColor;
    private BehaviorState state = BehaviorState.Waiting;
    private int groupIndex;
    private float formationTargetX;
    private float stateStartedAt;
    private float nextPounceAt;
    private float shuffleStartsAt;
    private bool shufflePending;
    private bool pounceHitResolved;
    private float pounceDirection;
    private static AudioClip sharedGrowlClip;

    public bool IsInChasePhase => state == BehaviorState.Chasing || state == BehaviorState.Reordering || state == BehaviorState.Pouncing || state == BehaviorState.Recovering;
    public bool IsReordering => state == BehaviorState.Reordering || shufflePending;

    private bool IsGrounded => groundContacts.Count > 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        baseScale = transform.localScale;
        if (sprite != null) baseColor = sprite.color;
        if (body != null)
        {
            body.freezeRotation = true;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        growlSource = GetComponent<AudioSource>();
        if (growlSource == null) growlSource = gameObject.AddComponent<AudioSource>();
        growlSource.playOnAwake = false;
        growlSource.spatialBlend = 0f;
        growlSource.volume = 0.22f;
    }

    public void Configure(Map001PlayerController target, Map001RuntimeDirector owner, Map001EnemyController[] members, int index)
    {
        player = target;
        director = owner;
        group = members;
        groupIndex = index;
    }

    public void BeginAppearance()
    {
        if (body == null) body = GetComponent<Rigidbody2D>();
        Collider2D enemyCollider = GetComponent<Collider2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (growlSource == null)
        {
            growlSource = GetComponent<AudioSource>();
            if (growlSource == null) growlSource = gameObject.AddComponent<AudioSource>();
            growlSource.playOnAwake = false;
            growlSource.spatialBlend = 0f;
            growlSource.volume = 0.22f;
        }

        state = BehaviorState.Appearing;
        stateStartedAt = Time.time;
        shufflePending = false;
        if (enemyCollider != null) enemyCollider.enabled = true;
        if (body != null)
        {
            body.simulated = true;
            body.linearVelocity = new Vector2(0f, 8.1f);
        }
        animator?.SetTrigger("Jump");
        Debug.Log("MAP-001: Khỉ Đột " + (groupIndex + 1) + " nhảy ra cùng cả đàn.");
    }

    public void SetFormationTarget(float x)
    {
        formationTargetX = x;
    }

    public void ScheduleShuffleHop(float targetX, float delay)
    {
        formationTargetX = targetX;
        if (state != BehaviorState.Chasing) return;

        shufflePending = true;
        shuffleStartsAt = Time.time + Mathf.Max(0f, delay);
    }

    private void Update()
    {
        if (body == null || state == BehaviorState.Waiting || state == BehaviorState.Frozen) return;

        switch (state)
        {
            case BehaviorState.Appearing:
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                if (Time.time - stateStartedAt > 0.2f && IsGrounded)
                    BeginGrowl();
                else if (Time.time - stateStartedAt > 1.6f)
                {
                    body.linearVelocity = new Vector2(0f, 0f);
                    BeginGrowl();
                }
                break;

            case BehaviorState.Growling:
                UpdateGrowlPose();
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                if (Time.time - stateStartedAt >= 1.35f)
                {
                    transform.localScale = baseScale;
                    if (sprite != null) sprite.color = baseColor;
                    state = BehaviorState.Chasing;
                    stateStartedAt = Time.time;
                    Debug.Log("MAP-001: Ba Khỉ Đột gầm gừ xong và bắt đầu đuổi theo người chơi.");
                }
                break;

            case BehaviorState.Chasing:
                if (shufflePending && Time.time >= shuffleStartsAt)
                {
                    BeginShuffleHop();
                    break;
                }
                MoveTowardFormation();
                TryPounce();
                break;

            case BehaviorState.Reordering:
                MoveTowardFormation(1.7f);
                if (Time.time - stateStartedAt > 0.2f && IsGrounded)
                {
                    state = BehaviorState.Chasing;
                    stateStartedAt = Time.time;
                }
                else if (Time.time - stateStartedAt > 1.45f)
                {
                    state = BehaviorState.Chasing;
                    stateStartedAt = Time.time;
                }
                break;

            case BehaviorState.Pouncing:
                UpdatePounce();
                break;

            case BehaviorState.Recovering:
                body.linearVelocity = new Vector2(GetSeparationSpeed(), body.linearVelocity.y);
                if (Time.time - stateStartedAt >= 0.48f)
                    state = BehaviorState.Chasing;
                break;
        }
    }

    private void BeginGrowl()
    {
        state = BehaviorState.Growling;
        stateStartedAt = Time.time;
        body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
        if (growlSource != null)
            growlSource.PlayOneShot(GetGrowlClip(), 1f);
        Debug.Log("MAP-001: Khỉ Đột " + (groupIndex + 1) + " gầm gừ trước khi rượt đuổi.");
    }

    private void UpdateGrowlPose()
    {
        float pulse = Mathf.Abs(Mathf.Sin((Time.time - stateStartedAt) * Mathf.PI * 3.2f));
        float phase = groupIndex * 0.12f;
        float breath = Mathf.Abs(Mathf.Sin((Time.time - stateStartedAt) * Mathf.PI * 2.7f + phase));
        transform.localScale = new Vector3(
            baseScale.x * (1f + 0.035f * breath),
            baseScale.y * (1f + 0.09f * pulse),
            baseScale.z);
        if (sprite != null)
            sprite.color = Color.Lerp(baseColor, new Color(1f, 0.68f, 0.52f, baseColor.a), pulse * 0.36f);
    }

    private void MoveTowardFormation(float speedMultiplier = 1f)
    {
        if (player == null) return;

        float dx = formationTargetX - transform.position.x;
        float desiredSpeed = Mathf.Abs(dx) > 0.28f ? Mathf.Sign(dx) * runSpeed * speedMultiplier : 0f;
        float separationSpeed = GetSeparationSpeed();
        body.linearVelocity = new Vector2(desiredSpeed + separationSpeed, body.linearVelocity.y);
        FacePlayer();
    }

    private float GetSeparationSpeed()
    {
        if (group == null) return 0f;
        float result = 0f;
        for (int i = 0; i < group.Length; i++)
        {
            Map001EnemyController other = group[i];
            if (other == null || other == this || !other.gameObject.activeInHierarchy || other.state == BehaviorState.Frozen) continue;
            if (Mathf.Abs(transform.position.y - other.transform.position.y) >= 1.78f) continue;

            float dx = transform.position.x - other.transform.position.x;
            float distance = Mathf.Abs(dx);
            if (distance >= MinimumStandingGap) continue;
            float side = distance > 0.02f ? Mathf.Sign(dx) : (groupIndex < other.groupIndex ? -1f : 1f);
            result += side * (MinimumStandingGap - distance) * 3.4f;
        }
        return Mathf.Clamp(result, -4.6f, 4.6f);
    }

    private void FacePlayer()
    {
        if (player == null) return;
        float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
        if (Mathf.Abs(direction) < 0.01f) return;
        Vector3 scale = baseScale;
        scale.x = Mathf.Abs(baseScale.x) * direction;
        transform.localScale = scale;
    }

    private void BeginShuffleHop()
    {
        shufflePending = false;
        state = BehaviorState.Reordering;
        stateStartedAt = Time.time;
        float direction = Mathf.Sign(formationTargetX - transform.position.x);
        body.linearVelocity = new Vector2(direction * runSpeed * 1.7f, ReorderJumpImpulse);
        animator?.SetTrigger("Jump");
    }

    private void TryPounce()
    {
        if (player == null || director == null || !IsGrounded || Time.time < nextPounceAt) return;
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (distance < 2.1f || distance > leapDistance) return;
        if (!director.TryClaimPounce(this)) return;

        nextPounceAt = Time.time + UnityEngine.Random.Range(4.6f, 6.2f);
        state = BehaviorState.Pouncing;
        stateStartedAt = Time.time;
        pounceHitResolved = false;
        pounceDirection = Mathf.Sign(player.transform.position.x - transform.position.x);
        if (Mathf.Abs(pounceDirection) < 0.01f) pounceDirection = 1f;
        float landingX = player.transform.position.x - pounceDirection * 0.62f;
        float horizontalSpeed = Mathf.Clamp((landingX - transform.position.x) / PounceFlightDuration, -PounceMaxSpeed, PounceMaxSpeed);
        body.linearVelocity = new Vector2(horizontalSpeed, PounceJumpImpulse);
        FacePlayer();
        animator?.SetTrigger("Jump");
        Debug.Log("MAP-001: Một Khỉ Đột lấy đà nhảy vồ sát người chơi.");
    }

    private void UpdatePounce()
    {
        if (player != null && Time.time - stateStartedAt < PounceFlightDuration)
        {
            float landingX = player.transform.position.x - pounceDirection * 0.62f;
            float remaining = Mathf.Max(0.08f, PounceFlightDuration - (Time.time - stateStartedAt));
            float horizontalSpeed = Mathf.Clamp((landingX - transform.position.x) / remaining, -PounceMaxSpeed, PounceMaxSpeed);
            body.linearVelocity = new Vector2(horizontalSpeed, body.linearVelocity.y);
        }

        float elapsed = Time.time - stateStartedAt;
        if ((elapsed > 0.25f && IsGrounded) || elapsed > PounceFlightDuration + 0.7f)
        {
            ResolvePounce();
            body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
            state = BehaviorState.Recovering;
            stateStartedAt = Time.time;
            director?.FinishPounce(this);
        }
    }

    private void ResolvePounce()
    {
        if (pounceHitResolved) return;
        pounceHitResolved = true;
        if (player == null) return;

        Vector2 separation = player.transform.position - transform.position;
        if (Mathf.Abs(separation.x) <= 1.35f && Mathf.Abs(separation.y) <= 1.8f)
        {
            player.TakeHit(transform.position);
            Debug.Log("MAP-001: Khỉ Đột đáp sát người chơi; đòn vồ gây choáng.");
        }
        else
        {
            Debug.Log("MAP-001: Người chơi né được cú nhảy vồ của Khỉ Đột.");
        }
    }

    public void Freeze()
    {
        if (state == BehaviorState.Pouncing) director?.FinishPounce(this);
        state = BehaviorState.Frozen;
        shufflePending = false;
        if (body != null)
        {
            body.linearVelocity = Vector2.zero;
            body.simulated = false;
        }
        if (sprite != null)
        {
            sprite.sortingOrder = 8;
            sprite.color = baseColor;
        }
        transform.localScale = baseScale;
        animator?.SetTrigger("Hurt");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.55f)
            {
                groundContacts.Add(collision.collider);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundContacts.Remove(collision.collider);
    }

    private static AudioClip GetGrowlClip()
    {
        if (sharedGrowlClip != null) return sharedGrowlClip;

        const int sampleRate = 22050;
        const float duration = 0.62f;
        int sampleCount = Mathf.CeilToInt(sampleRate * duration);
        float[] samples = new float[sampleCount];
        System.Random noise = new System.Random(8173);
        float filteredNoise = 0f;
        for (int i = 0; i < sampleCount; i++)
        {
            float time = i / (float)sampleRate;
            float envelope = Mathf.Min(1f, time / 0.055f) * Mathf.Pow(Mathf.Clamp01((duration - time) / 0.2f), 0.65f);
            float wobble = 0.62f * Mathf.Sin(2f * Mathf.PI * 3.1f * time);
            float rumble = Mathf.Sin(2f * Mathf.PI * 57f * time + wobble);
            float rasp = 0.34f * Mathf.Sin(2f * Mathf.PI * 91f * time + wobble * 1.4f);
            filteredNoise = filteredNoise * 0.91f + ((float)noise.NextDouble() * 2f - 1f) * 0.09f;
            samples[i] = Mathf.Clamp((rumble + rasp + filteredNoise * 0.42f) * envelope * 0.55f, -1f, 1f);
        }

        sharedGrowlClip = AudioClip.Create("TiengGamKhiDot_MAP001", sampleCount, 1, sampleRate, false);
        sharedGrowlClip.SetData(samples, 0);
        return sharedGrowlClip;
    }
}
