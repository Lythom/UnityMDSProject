using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
public class MoveCharacter : MonoBehaviour {
    public enum PlayerControls {
        ZQSDF,
        ArrowAnd0
    }

    public float ShootStrength = 5;

    public GameObject ShootFX;
    public ParticleSystem DustWalkFX;
    public float DustAmount = 10;
    private const string ROLL = "roll";

    [Tooltip("Speed in Unit per second")] public float speed = 5f;

    private SpriteRenderer spriteRenderer;
    private SpritesheetAnimator animator;
    private Rigidbody2D body;

    public PlayerControls controls = PlayerControls.ArrowAnd0;

    // COOLDOWNS
    [Tooltip("Cooldown of a roll in seconds")]
    public float rollCooldownDuration = 1;
    private float rollCooldown = 0;

    private float controlLevel = 1; // 0 no control, 1=full control
    public float ControlRecoverySpeed = 3;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<SpritesheetAnimator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.rigidbody.bodyType == RigidbodyType2D.Dynamic) {
            if (this.animator.CurrentAnimation.name == Anims.Roll) {
                other.rigidbody.AddForce(-other.GetContact(0).normal * ShootStrength, ForceMode2D.Impulse);
                Instantiate(ShootFX, other.GetContact(0).point, Quaternion.identity);
            }
        }
        controlLevel = 0;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector2 vitesse = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow) && controls == PlayerControls.ArrowAnd0
        || Input.GetKey(KeyCode.Z) && controls == PlayerControls.ZQSDF) {
            vitesse += Vector2.up;
        }

        if (Input.GetKey(KeyCode.DownArrow) && controls == PlayerControls.ArrowAnd0
        || Input.GetKey(KeyCode.S) && controls == PlayerControls.ZQSDF) {
            vitesse += Vector2.down;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && controls == PlayerControls.ArrowAnd0
        || Input.GetKey(KeyCode.Q) && controls == PlayerControls.ZQSDF) {
            vitesse += Vector2.left;
            spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow) && controls == PlayerControls.ArrowAnd0
        || Input.GetKey(KeyCode.D) && controls == PlayerControls.ZQSDF) {
            vitesse += Vector2.right;
            spriteRenderer.flipX = false;
        }

        if ((Input.GetKeyDown(KeyCode.Keypad0) && controls == PlayerControls.ArrowAnd0
        || Input.GetKey(KeyCode.F) && controls == PlayerControls.ZQSDF) && rollCooldown <= 0) {
            animator.Play(Anims.Roll);
            rollCooldown = rollCooldownDuration;
        }

        if (animator.CurrentAnimation.name != Anims.Roll || animator.LoopCount >= 1) {
            if (vitesse.magnitude > 0) {
                animator.Play(Anims.Run);
            } else {
                animator.Play(Anims.Iddle);
            }
        }

        controlLevel = Mathf.Clamp01(controlLevel + Time.deltaTime * ControlRecoverySpeed);
        body.velocity = Vector2.Lerp(body.velocity, vitesse.normalized * speed, (controlLevel < 0.05f) ? 0.05f : Mathf.Pow(2, 10 * (controlLevel - 1)));
        var emission = DustWalkFX.emission;
        emission.rateOverTime = vitesse.magnitude * DustAmount;

        rollCooldown -= Time.deltaTime;
    }
}