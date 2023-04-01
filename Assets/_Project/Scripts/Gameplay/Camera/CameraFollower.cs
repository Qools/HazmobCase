using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject ball;

    [SerializeField] private float smoothTime = 0.25f;

    [SerializeField] private float depth = -10f;

    [SerializeField] private int offset = 2;

    Vector3 currentVelocity;

    float lastPosY;

    private float startHeight;

    // Start is called before the first frame update
    void Start()
    {
        lastPosY = transform.position.y;

        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ball == null)
        {
            return;
        }

        if (GameManager.Instance.isGameStarted)
        {
            currentVelocity = new Vector3(0f, ball.GetComponent<Rigidbody2D>().velocity.y, 0f);

            transform.position = Vector3.SmoothDamp(
                transform.position, 
                new Vector3(0f, ball.transform.position.y - offset, depth),
                ref currentVelocity,
                smoothTime);

            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y, lastPosY, float.PositiveInfinity),
                transform.position.z
            );

            lastPosY = transform.position.y;

            EventSystem.CallScoreChange((int)lastPosY - offset);
        }
    }

    private void OnEnable()
    {
        EventSystem.OnRetryButtonPressed += OnRetryButtonPressed;
    }

    private void OnDisable()
    {
        EventSystem.OnRetryButtonPressed -= OnRetryButtonPressed;
    }

    private void OnRetryButtonPressed()
    {
        lastPosY = startHeight;

        transform.position = new Vector3(
                transform.position.x,
                startHeight,
                transform.position.z
            );
    }
}
