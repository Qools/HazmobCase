using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.25f;

    [SerializeField] private float depth = 0f;

    [SerializeField] private float offset = 2f;

    Vector3 currentVelocity;

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Instance.isGameStarted)
        {
            currentVelocity = new Vector3(
                0f, 
                Camera.main.transform.position.y, 
                0f
                );

            transform.position = Vector3.SmoothDamp(
                transform.position,
                new Vector3(0f, Camera.main.transform.position.y - offset, depth),
                ref currentVelocity,
                smoothTime
                );
        }
    }
}
