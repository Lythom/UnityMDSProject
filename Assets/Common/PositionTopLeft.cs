using UnityEngine;

[ExecuteInEditMode]
public class PositionTopLeft : MonoBehaviour {
    public new Camera camera;
    public Vector2 offset;

    void Update() {
        Vector2 cameraPosition = camera.transform.position;
        float viewportHeight = camera.orthographicSize * 2;
        float top = cameraPosition.y + viewportHeight / 2;
        float viewportWidth = camera.aspect * camera.orthographicSize * 2;
        float left = cameraPosition.x - viewportWidth / 2;
        this.transform.position = new Vector3(left + offset.x, top + offset.y, this.transform.position.z);
    }
}