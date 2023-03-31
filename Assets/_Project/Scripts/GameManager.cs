using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coins;
    [SerializeField] private int coinMultiplier;

    public int score;
    public int highScore;

    public bool isGameStarted = false;
    public bool isGameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventSystem.OnGameStarted += OnGameStarted;
        EventSystem.OnIncreaseScore += OnIncreaseScore;
        EventSystem.OnCoinPickUp += OnCoinPickUp;
        EventSystem.OnGameOver += OnGameOver;
    }


    private void OnDisable()
    {
        EventSystem.OnGameStarted -= OnGameStarted;
        EventSystem.OnIncreaseScore -= OnIncreaseScore;
        EventSystem.OnCoinPickUp -= OnCoinPickUp;
        EventSystem.OnGameOver -= OnGameOver;
    }
    private void OnGameStarted()
    {
        isGameStarted = true;
        isGameOver = false;

        EventSystem.CallCoinUIRefresh(coins);
    }

    private void OnIncreaseScore(float _score)
    {
        score = (int)_score;
    }

    private void OnCoinPickUp()
    {
        coins++;
        EventSystem.CallCoinUIRefresh(coins);
    }

    private void OnGameOver()
    {
        isGameStarted = false;
        isGameOver = true;

        int value = coins * coinMultiplier;
        AddCurrency(value);
    }

    public void AddCurrency(int _value)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = PlayerPrefKeys.gold,
            Amount = _value
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnError);
    }

    private void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        int currency = result.Balance;
        EventSystem.CallCurrencyRefresh(currency);
        Debug.Log("Currency " + currency);
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.Error);
    }
}
