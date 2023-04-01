using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GameManager : Singleton<GameManager>
{
    public LevelList levelList;

    [HideInInspector]
    public GameObject currentLevel;

    public int coins;
    [SerializeField] private int coinMultiplier;

    public int score;
    public int highScore;

    public bool isGameStarted = false;
    public bool isGameOver = false;

    public Ball selectedBall;


    // Start is called before the first frame update
    void Start()
    {
        WaitInit(Init);

        coins = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventSystem.OnGameStarted += OnGameStarted;
        EventSystem.OnGameOver += OnGameOver;
        
        EventSystem.OnScoreChange += OnScoreChange;
        EventSystem.OnCoinPickUp += OnCoinPickUp;

        EventSystem.OnPlayButtonPressed += LoadLevel;
        EventSystem.OnRetryButtonPressed += OnRetry;

        EventSystem.OnBallSelected += OnBallSelected;
    }


    private void OnDisable()
    {
        EventSystem.OnGameStarted -= OnGameStarted;
        EventSystem.OnGameOver -= OnGameOver;
        
        EventSystem.OnScoreChange -= OnScoreChange;
        EventSystem.OnCoinPickUp -= OnCoinPickUp;

        EventSystem.OnPlayButtonPressed -= LoadLevel;
        EventSystem.OnRetryButtonPressed -= OnRetry;

        EventSystem.OnBallSelected -= OnBallSelected;
    }

    public void Init()
    {
        SetStatus(Status.ready);
    }

    public void LoadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levelList.LoopLevelsByIndex(1));

        //EventSystem.CallLevelLoaded();
        //EventSystem.CallCoinUIRefresh(coins);
        //EventSystem.CallScoreChange(score);

        StartCoroutine(WaitForLevelLoad());
    }

    private IEnumerator WaitForLevelLoad()
    {
        yield return new WaitWhile(() => currentLevel == null);


        EventSystem.CallLevelLoaded();
        EventSystem.CallCoinUIRefresh(coins);
        EventSystem.CallScoreChange(score);
    }

    private void OnGameStarted()
    {
        isGameStarted = true;
        isGameOver = false;

        EventSystem.CallCoinUIRefresh(coins);
    }

    private void OnScoreChange(float _score)
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

    private void OnRetry()
    {
        LoadLevel();

        isGameStarted = false;
        isGameOver = false;

        score = 0;
        coins = 0;

        EventSystem.CallCoinUIRefresh(coins);
        EventSystem.CallScoreChange(score);
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

    private void OnBallSelected(Ball ball)
    {
        selectedBall = ball;
    }
}
