using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class GameOverView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject highScoreScreen;
    [SerializeField] private TextMeshProUGUI highScoreScreenText;

    [SerializeField] private Button retryButton;

    private int highScore;

    private void Awake()
    {
        if (this.TryGetComponent(out CanvasGroup _canvasGroup))
        {
            canvasGroup = _canvasGroup;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(OnRetryButtonClicked);

        Utility.EnablePanel(canvasGroup, false);
    }

    private void OnEnable()
    {
        EventSystem.OnGameOver += OnGameOver;
        //EventSystem.OnSuccesfullLogin += OnLogin;
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
        //EventSystem.OnSuccesfullLogin -= OnLogin;
    }

    //private void OnLogin()
    //{
    //    SendLeaderboard(GameManager.Instance.score);
    //}


    private void OnGameOver()
    {
        Utility.EnablePanel(canvasGroup, true);

        if (GameManager.Instance.score > GameManager.Instance.highScore)
        {
            EnableHighScoreScreen();

            GameManager.Instance.highScore = GameManager.Instance.score;

            SendLeaderboard(GameManager.Instance.score);
        }

        else
        {
            EnableScoreScreen();
        }
    }

    private void OnRetryButtonClicked()
    {
        EventSystem.CallRetryButtonPressed();

        Utility.EnablePanel(canvasGroup, false);
    }

    

    private void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate
                {
                    StatisticName = PlayerPrefKeys.leaderBoardName,
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSendLeaderBoardSuccess, OnError);
    }

    private void OnSendLeaderBoardSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard updated");
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.Error);
    }

    private void EnableScoreScreen()
    {
        highScoreScreen.SetActive(false);

        scoreScreen.SetActive(true);
        scoreText.text = PlayerPrefKeys.scoreText + "\n" + GameManager.Instance.score.ToString("F0"); 
    }

    private void EnableHighScoreScreen()
    {
        scoreScreen.SetActive(false);

        highScoreScreen.SetActive(true);
        highScoreScreenText.text = PlayerPrefKeys.highScoreText + "\n" + GameManager.Instance.score.ToString("F0");
    }
}
