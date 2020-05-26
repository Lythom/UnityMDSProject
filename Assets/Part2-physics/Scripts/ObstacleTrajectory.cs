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
    void Update() {
        lastPos = rb.position;
        rb.position = new Vector3(
            Mathf.Cos((Offset + Time.time * Speed) * Mathf.PI * 2) * Radius,
            Mathf.Sin((Offset + Time.time * Speed) * Mathf.PI * 2) * Radius,
            0
        );
        rb.velocity = (rb.position - lastPos) / Time.deltaTime;
    }
}
