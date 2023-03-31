using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUpTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerPrefKeys.ballTag))
        {
            EventSystem.CallCoinPickUp();

            Destroy(this.gameObject);
        }
    }
}
