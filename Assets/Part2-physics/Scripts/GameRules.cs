using System;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    // 2 joueurs poussent un ballon
    // si le ballon touche un but
    // => score pour l'équipe qui marque (équpe adverse de celle qui prend le but)
    // => on réenge: remise à la position initiale des joueurs et de la balle

    [Serializable]
    public struct Entity
    {
        public GameObject gameObject;
        [HideInInspector]
        public Vector3 initialPosition;
    }

    [Serializable]
    public struct Team
    {
        public GameObject goal;
        public Text score;
    }

    public Entity[] players;
    public Entity ball;
    public Team orange;
    public Team blue;

    private int orangeScore = 0;
    private int blueScore = 0;

    void Start()
    {
        RecordInitialPositions();
        UpdateScores();

        BallCollisionEmitter emitter = ball.gameObject.GetComponentInChildren<BallCollisionEmitter>();
        emitter.OnCollided += BallCollided;
    }

    void Destroy() {
        BallCollisionEmitter emitter = ball.gameObject.GetComponentInChildren<BallCollisionEmitter>();
        emitter.OnCollided -= BallCollided;
    }

    private void BallCollided(Collision2D collision)
    {
        if (collision.collider.gameObject.name == orange.goal.name)
        {
            blueScore++;
            UpdateScores();
            ResetPositions();
        }
        else if (collision.collider.gameObject.name == blue.goal.name)
        {
            orangeScore++;
            UpdateScores();
            ResetPositions();
        }
    }

    private void ResetPositions()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.transform.position = players[i].initialPosition;
        }
        ball.gameObject.transform.position = ball.initialPosition;
        ball.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void RecordInitialPositions()
    {
        // On mémorise les positions initiales
        for (int i = 0; i < players.Length; i++)
        {
            players[i].initialPosition = players[i].gameObject.transform.position;
        }
        ball.initialPosition = ball.gameObject.transform.position;
    }

    public void UpdateScores()
    {
        orange.score.text = orangeScore.ToString();
        blue.score.text = blueScore.ToString();
    }
}
