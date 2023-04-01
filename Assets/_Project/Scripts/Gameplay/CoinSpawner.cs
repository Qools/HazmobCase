using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> coins = new List<GameObject>();
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinParent;

    [SerializeField] private int coinCount;
    [SerializeField] private float spawnDistance;

    [SerializeField] private float xClamp;


    private void OnEnable()
    {
        EventSystem.OnLevelLoaded += OnLeveLoaded;
        EventSystem.OnRetryButtonPressed += OnRetryButtonPressed;
    }

    private void OnDisable()
    {
        EventSystem.OnLevelLoaded -= OnLeveLoaded;
        EventSystem.OnRetryButtonPressed -= OnRetryButtonPressed;
    }

    private void OnLeveLoaded()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 spawnPosition = new Vector3(
                    Random.Range(-xClamp, xClamp),
                    (i+1) * spawnDistance,
                    0f
                );
            GameObject spawnedCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, coinParent);

            coins.Add(spawnedCoin);
        }
    }

    private void OnRetryButtonPressed()
    {
        foreach (var item in coins)
        {
            Destroy(item.gameObject);
        }

        coins = new List<GameObject>();
    }
}
