using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGamePanelView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (this.TryGetComponent(out CanvasGroup _canvasGroup))
        {
            canvasGroup = _canvasGroup;
        }
    }

    private void Start()
    {
        Utility.EnablePanel(canvasGroup, false);
    }

    private void OnEnable()
    {
        EventSystem.OnGameStarted += OnGameStarted;
        EventSystem.OnIncreaseScore += OnIncreaseScore;
        EventSystem.OnCoinUIRefresh += OnCoinPickUp;
    }

    private void OnDisable()
    {
        EventSystem.OnGameStarted -= OnGameStarted;
        EventSystem.OnIncreaseScore -= OnIncreaseScore;
        EventSystem.OnCoinUIRefresh -= OnCoinPickUp;
    }

    private void OnGameStarted()
    {
        Utility.EnablePanel(canvasGroup, true);
    }

    private void OnIncreaseScore(float score)
    {
        scoreText.text = score.ToString("F0") + "m";
    }

    private void OnCoinPickUp(int _coins)
    {
        coinText.text = _coins.ToString();
    }
}
