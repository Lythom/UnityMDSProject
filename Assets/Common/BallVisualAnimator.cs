using UnityEngine;

[RequireComponent(typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
public class BallVisualAnimator : MonoBehaviour {
    public int animtionSpeedRatio = 3;

    private Rigidbody2D body;
    private SpritesheetAnimator animator;

    void Start() {
        animator = GetComponent<SpritesheetAnimator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 vitesse = body.velocity;
        float amplitude = vitesse.magnitude;
        animator.animationSpeed = amplitude * animtionSpeedRatio;
        body.rotation = Mathf.Rad2Deg * Mathf.Atan2(vitesse.y, vitesse.x);
    }
}