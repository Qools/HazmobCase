using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Ball selectedBall;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float force;

    private Vector3 lastVelocity;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    rb.AddForce(Vector2.up * force);
        //}
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnEnable()
    {
        EventSystem.OnBallSelected += OnBallSelected;
    }

    private void OnDisable()
    {
        EventSystem.OnBallSelected -= OnBallSelected;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(PlayerPrefKeys.barrier) || collision.transform.CompareTag(PlayerPrefKeys.stick))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * speed;
        }
    }

    private void OnBallSelected(Ball ball)
    {
        selectedBall = ball;

        SetBall();
    }

    private void SetBall()
    {
        if (selectedBall.isUnlocked && selectedBall.isSelected)
        {
            transform.localScale = new Vector2(selectedBall.ballSize, selectedBall.ballSize);
            spriteRenderer.color = Utility.GetColorFromString(selectedBall.ballColor);
            rb.mass = selectedBall.ballWeight;
        }
    }
}
