using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public float velocity;

    private Vector2 startPos;

    public float maxClampValue;
    public float minClampValue;

    private void Update()
    {
        transform.localPosition = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.localPosition.y, minClampValue, maxClampValue),
            transform.position.z
            );
    }

    public void OnMovementStart(Vector2 pos)
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        startPos = pos;
    }

    public void OnMovement(Vector3 pos)
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        velocity = Mathf.Abs((startPos.y - pos.y) / Time.deltaTime) /*/ 10f*/;
    }

    public void OnMovementEnd(Vector2 pos)
    {
        if (!GameManager.Instance.isGameStarted)
        {
            return;
        }

        velocity = 0f;

        startPos = pos;
    }
}
