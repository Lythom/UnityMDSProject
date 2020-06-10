using UnityEngine;

public class ObstacleTrajectory : MonoBehaviour {

    public float Offset = 0;
    public float Speed = 0.1f;
    public float Radius = 3;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 lastPos = Vector2.zero;

    // Utilise FixedUpdate pour manipuler la physique
    void FixedUpdate() {
        // calcule de la prochaine position
        lastPos = rb.position;
        rb.position = new Vector3(
            Mathf.Cos((Offset + Time.time * Speed) * Mathf.PI * 2) * Radius,
            Mathf.Sin((Offset + Time.time * Speed) * Mathf.PI * 2) * Radius,
            0
        );
        // astuce pour avoir un calcule de vélocité qui impacte la physique : calcule une vélocité et repositionne le kinematic à sa dernière position
        rb.velocity = (rb.position - lastPos) / Time.deltaTime;
        rb.position = lastPos;
    }
}
