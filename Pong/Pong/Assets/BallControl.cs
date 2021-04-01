using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody2D rigidBody2D;

    public float xInitialForce;
    public float yInitialForce;

    private Vector2 trajectoryOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        RestartGame();

        trajectoryOrigin = transform.position;
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //Menentukan arah bola saat di inisiasi
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        //membuat vektor positif dari vektor x dan y
        float absYSpeed = Mathf.Abs(yRandomInitialForce);
        Vector2 AbsoluteSpeed = new Vector2(Mathf.Abs(xInitialForce), absYSpeed);

        //Memastikan Resultan Gaya konstan
        float InitialMagnitude = AbsoluteSpeed.magnitude;

        //Menghitung kecepatan x tergantung dari nilai Y agar magnitude konstan
        float xSpeed = Mathf.Sqrt(InitialMagnitude * InitialMagnitude - absYSpeed * absYSpeed);

        if (randomDirection < 1.0f)
        {
            rigidBody2D.AddForce(new Vector2(-xSpeed, yRandomInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xSpeed, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }


}
