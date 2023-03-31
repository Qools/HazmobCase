using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerPrefKeys.ballTag))
        {
            if (!GameManager.Instance.isGameOver)
            {
                EventSystem.CallGameOver();
            }        }
    }
}
