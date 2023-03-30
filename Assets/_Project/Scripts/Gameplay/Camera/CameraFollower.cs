using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform ball;

    [SerializeField] private float smoothTime = 0.25f;

    [SerializeField] private float depth = -10f;

    private bool isGameStarted = false;

    Vector3 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        isGameStarted = false;

        ball = GameObject.FindWithTag(PlayerPrefKeys.ballTag).transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isGameStarted)
        {
            currentVelocity = new Vector3(0f, ball.GetComponent<Rigidbody2D>().velocity.y, 0f);

            transform.position = Vector3.SmoothDamp(
                transform.position, 
                new Vector3(0f, ball.position.y, depth),
                ref currentVelocity,
                smoothTime);
        }
    }

    private void OnEnable()
    {
        EventSystem.OnGameStarted += OnGameStart;
        EventSystem.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventSystem.OnGameStarted -= OnGameStart;
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameStart()
    {
        isGameStarted = true;
    }

    private void OnGameOver()
    {
        isGameStarted = false;
    }
}
