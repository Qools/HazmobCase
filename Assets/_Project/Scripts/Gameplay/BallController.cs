using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] public Ball selectedBall;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public TrailRenderer trailRenderer;

    private Vector3 lastVelocity;

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnEnable()
    {
        EventSystem.OnLevelLoaded += SetBall;
        EventSystem.OnGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        EventSystem.OnLevelLoaded -= SetBall;
        EventSystem.OnGameStarted -= OnGameStarted;
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

                rb.AddForce(direction * (speed + stickController.velocity));
            }
        }
    }

    private void SetBall()
    {
        selectedBall = GameManager.Instance.selectedBall;

        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        if (selectedBall.isUnlocked && selectedBall.isSelected)
        {
            transform.localScale = new Vector2(selectedBall.ballSize, selectedBall.ballSize);
            spriteRenderer.color = Utility.GetColorFromString(selectedBall.ballColor);
            rb.mass = selectedBall.ballWeight;

            colorKey = new GradientColorKey[3];
            colorKey[0].color = Utility.GetColorFromString(selectedBall.ballColor);
            colorKey[0].time = 0.0f;
            colorKey[1].color = Utility.GetColorFromString(selectedBall.ballColor);
            colorKey[1].time = 0.66f;
            colorKey[2].color = Utility.GetColorFromString("000000");
            colorKey[2].time = 1f;

            alphaKey = new GradientAlphaKey[3];
            alphaKey[0].alpha = 1.0f;
            alphaKey[0].time = 0.0f;
            alphaKey[1].alpha = 0.36f;
            alphaKey[1].time = 0.66f;
            alphaKey[2].alpha = 0.0f;
            alphaKey[2].time = 1.0f;

            gradient.SetKeys(colorKey, alphaKey);

            trailRenderer.colorGradient = gradient;
        }
    }

    private void OnGameStarted()
    {
        EnablePhysics();
        SetBallToCamera();
    }

    private void EnablePhysics()
    {
        rb.simulated = true;

        Vector2 negativeRandom = new Vector2(Random.Range(-10f, -5f), 5);
        Vector2 positiveRandom = new Vector2(Random.Range(5f, 10f), 5);

        List<Vector2> values = new List<Vector2>();
        values.Add(negativeRandom);
        values.Add(positiveRandom);

        rb.AddForce(values[Random.Range(0, 2)] * 10);
    }

    private void SetBallToCamera()
    {
        if (Camera.main.TryGetComponent(out CameraFollower cameraFollower))
        {
            cameraFollower.ball = gameObject;
        }
    }
}
