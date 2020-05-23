using System;
using System.Linq;
using UnityEngine;

// Lister toutes les animations possibles ici
public enum Anims {
    Iddle,
    Run,
    Roll
}

// Structure de données pour définir une animation
[Serializable]
public struct AnimDefinition {
    public Anims name;
    public Sprite[] frames;
}

// L'animator va changer le sprite du SpriteRenderer à une certaine fréquence (par défaut 25 fois par secondes)
[RequireComponent(typeof(SpriteRenderer))]
public class SpritesheetAnimator : MonoBehaviour {

    [Tooltip("animation speed in images per seconds")]
    public float animationSpeed = 25;

    public AnimDefinition[] animations;

    // accessors (getters)
    public AnimDefinition CurrentAnimation => currentAnimation;
    public int LoopCount => loopCount;
    public float animationFrameDuration => 1f / animationSpeed;

    // private properties
    private SpriteRenderer spriteRenderer;
    private AnimDefinition currentAnimation;

    private int currentFrameIndex = 0;

    // durée en seconde avant l'affichage de la prochaine image
    private float nextFrameCoolDown;

    // nombre de fois que l'animation est jouée complètement
    private int loopCount = 0;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextFrameCoolDown = animationFrameDuration;
        currentAnimation = animations[0];
    }

    void Update() {
        if (currentAnimation.frames.Length == 0) return;

        // déclenche la frame suivante si le temps est écoulé
        if (nextFrameCoolDown <= 0) {
            AnimateNextFrame();
        }

        // si on change la vitesse de l'animation, raccourci le temps d'attente si nécessaire
        if (animationFrameDuration < nextFrameCoolDown) nextFrameCoolDown = animationFrameDuration;
        
        // écoule le temps avant la prochaine image
        nextFrameCoolDown -= Time.deltaTime;
    }

    public void AnimateNextFrame() {
        // calcule l'indice de l'image suivante
        currentFrameIndex = (currentFrameIndex + 1) % currentAnimation.frames.Length;
        // affiche l'image suivante
        spriteRenderer.sprite = currentAnimation.frames[currentFrameIndex];
        // Ajoute un temps d'attente calculé à partir de la fréquence de l'animation
        nextFrameCoolDown += animationFrameDuration;
        // compte le nombre de fois que l'animation a été jouée
        if (currentFrameIndex == 0) loopCount++;
    }

    public void Play(Anims nextAnimation) {
        // Si l'animation est déjà jouée, on ne fait rien
        if (currentAnimation.name == nextAnimation) return;
        // trouve l'animation correspondante dans la liste
        currentAnimation = animations.First(a => a.name == nextAnimation);
        // réinitialise à la première image de la séquence
        currentFrameIndex = 0;
        loopCount = 0;
    }
}
