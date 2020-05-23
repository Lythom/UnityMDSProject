using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
public class MoveCharacter : MonoBehaviour {
    public enum PlayerControls {
        ZQSDF,
        ArrowAnd0
    }
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

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<SpritesheetAnimator>();
        body = GetComponent<Rigidbody2D>();

        IEnumerable<Transform> children = (IEnumerable<Transform>) this.transform;
        foreach (Transform child in children) {
            child.gameObject.GetComponent<SpriteRenderer>(); // peut être null !
        }
        // méthode 2
        foreach (Transform child in this.transform) {
            child.gameObject.GetComponent<SpriteRenderer>(); // peut être null !
        }
        // méthode 3
        foreach (SpriteRenderer script in this.GetComponentsInChildren<SpriteRenderer>()) {
            // script n'est pas null
        }
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

        body.velocity = vitesse.normalized * speed;

        rollCooldown -= Time.deltaTime;
    }
}