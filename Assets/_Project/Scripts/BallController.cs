using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public BallAttributes selectedBall;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public float force;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector2.up * force);
        }
    }

    private void OnEnable()
    {
        EventSystem.OnBallSelected += OnBallSelected;
    }

    private void OnDisable()
    {
        EventSystem.OnBallSelected -= OnBallSelected;
    }

    private void OnBallSelected(BallAttributes ball)
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
