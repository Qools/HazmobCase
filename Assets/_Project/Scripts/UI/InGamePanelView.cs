using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanelView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

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
    }

    private void OnDisable()
    {
        EventSystem.OnGameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        Utility.EnablePanel(canvasGroup, true);
    }
}
