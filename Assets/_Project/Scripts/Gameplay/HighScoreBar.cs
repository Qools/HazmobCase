using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private int offset = 4;

    private void OnEnable()
    {
        EventSystem.OnLevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        EventSystem.OnLevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        if (GameManager.Instance.highScore < 1)
        {
            spriteRenderer.enabled = false;
            return;
        }

        transform.position = new Vector3(
                transform.position.x,
                GameManager.Instance.highScore + offset,
                transform.position.z
            );
    }
}
