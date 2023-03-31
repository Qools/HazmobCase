using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Ball selectedBall;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    private Vector3 lastVelocity;

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
        if (collision.transform.CompareTag(PlayerPrefKeys.barrier))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.AddForce(direction * speed);
        }

        if (collision.transform.CompareTag(PlayerPrefKeys.stick))
        {
            if (collision.transform.TryGetComponent(out StickController stickController))
            {
                var speed = lastVelocity.magnitude;
                var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                rb.AddForce(direction * (/*speed +*/ stickController.velocity));
            }
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
