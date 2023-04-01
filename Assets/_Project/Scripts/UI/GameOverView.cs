using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private GameObject highScoreScreen;

    [SerializeField] private Button retryButton;

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
    }

    private void OnDisable()
    {
        EventSystem.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        Utility.EnablePanel(canvasGroup, true);
    }

    private void OnRetryButtonClicked()
    {
        EventSystem.CallRetryButtonPressed();

        Utility.EnablePanel(canvasGroup, false);
    }
}
