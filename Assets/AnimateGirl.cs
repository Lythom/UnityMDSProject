using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateGirl : MonoBehaviour {

    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {

        if (Input.GetKey(KeyCode.RightArrow)) {
            animator.SetFloat("Speed", 2f);
        } else {
            animator.SetFloat("Speed", 0f);
        }

    }
}
