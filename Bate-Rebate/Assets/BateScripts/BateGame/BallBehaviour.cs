using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallBehaviour : MonoBehaviour {

	// Use this for initialization
    public float speed = 15f;
    public float timeToLaunch = 3f;
    public Vector3 posInit;

    public int pointsLeft = 0;
    public int pointsRight = 0;
    public GameObject scoreLeftShadow;
    public GameObject scoreLeftOver;
    public GameObject scoreRightOver;
    public GameObject scoreRightShadow;

    public bool paused = false;
    private Vector2 lastVelocity;

    void Start()
    {
        posInit = this.gameObject.transform.position;
        InvokeResetBall();
    }
    public void InvokeResetBall()
    {
        if (!paused)
        {
            this.gameObject.transform.position = posInit;
            rigidbody2D.velocity = Vector2.zero.normalized;
            Invoke("StartBall", timeToLaunch);
        }
    }
    public void ScorePoint(int player)
    {
        if (player == 1)
        {
            pointsLeft++;
            this.scoreLeftOver.GetComponent<Text>().text = pointsLeft.ToString();
            this.scoreLeftShadow.GetComponent<Text>().text = pointsLeft.ToString();
        }
        else if (player == 2)
        {
            pointsRight++;
            this.scoreRightOver.GetComponent<Text>().text = pointsRight.ToString();
            this.scoreRightShadow.GetComponent<Text>().text = pointsRight.ToString();
        }
    }
    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            lastVelocity = rigidbody2D.velocity;
            rigidbody2D.velocity = Vector2.zero.normalized;
        }
        else
        {
            paused = false;
            rigidbody2D.velocity = lastVelocity;
        }
    }

    public void StartBall()
    {
        int randomX = Random.Range(0, 4);
        Vector2 vel = Vector2.one.normalized;
        switch(randomX)
        {
            case 0:
                vel.x *= -speed;
                vel.y *= -speed;
                break;
            case 1:
                vel.x *= speed;
                vel.y *= speed;
                break;
            case 2:
                vel.x *= speed;
                vel.y *= -speed;
                break;
            case 3:
                vel.x *= -speed;
                vel.y *= speed;
                break;
        }
        rigidbody2D.velocity = vel;
        transform.Rotate(Vector3.forward * -5);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!paused)
        {
            if (col.gameObject.name == "paddleLeft")
            {
                float y = hitFactor(transform.position, col.transform.position, ((BoxCollider2D)col.collider).size.y);
                Vector2 dir = new Vector2(1, y).normalized;
                rigidbody2D.velocity = dir * speed;
            }
            if (col.gameObject.name == "paddleRight") 
            {
                float y = hitFactor(transform.position,col.transform.position,((BoxCollider2D)col.collider).size.y);
                Vector2 dir = new Vector2(-1, y).normalized;
                rigidbody2D.velocity = dir * speed;
            }
            if (col.gameObject.name == "wallR")
            {
                InvokeResetBall();
                ScorePoint(1);
                //P1 Scores!
            }
            if (col.gameObject.name == "wallL")
            {
                InvokeResetBall();
                ScorePoint(2);
                //P2 Scores!
            }
        }
    }
    float hitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
    {
        return (ballPos.y - paddlePos.y) / paddleHeight;
    }
	void Update () {
	}
}
