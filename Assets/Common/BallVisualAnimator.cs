using UnityEngine;

[RequireComponent(typeof(SpritesheetAnimator), typeof(Rigidbody2D))]
public class BallVisualAnimator : MonoBehaviour {
    public int animtionSpeedRatio = 3;
    public ParticleSystem ShieldParticleSystem;
    public ParticleSystem SpeedParticleSystem;
    public float HighSpeedEffectTriggerThreshold = 2;

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
        
        ShieldParticleSystem.gameObject.SetActive(amplitude > HighSpeedEffectTriggerThreshold);

        var speedEmission = SpeedParticleSystem.emission;
        speedEmission.rateOverTime = amplitude * 3;

        var shieldEmission = ShieldParticleSystem.emission;
        shieldEmission.rateOverTime = Mathf.Clamp(amplitude * 200 - HighSpeedEffectTriggerThreshold * 200, 0, 1000);
        
    }
}