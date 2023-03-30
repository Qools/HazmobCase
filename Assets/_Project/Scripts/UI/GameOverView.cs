using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

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
}
