using UnityEngine;

public class BallCollisionEmitter : MonoBehaviour
{
    public delegate void OnCollidedCallback(Collision2D collider);
    public event OnCollidedCallback OnCollided;

    void OnCollisionEnter2D(Collision2D collision) {
        OnCollided(collision);
    }
}
