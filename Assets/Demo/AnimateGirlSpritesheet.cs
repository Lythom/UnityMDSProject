using UnityEngine;

[RequireComponent(typeof(SpritesheetAnimator))]
public class AnimateGirlSpritesheet : MonoBehaviour {
    SpritesheetAnimator animator;

    void Start() {
        animator = GetComponent<SpritesheetAnimator>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            animator.Play(Anims.Run);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            animator.Play(Anims.Roll);
        } else {
            animator.Play(Anims.Iddle);
        }
    }
}
