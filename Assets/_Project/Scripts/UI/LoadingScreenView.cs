using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenView : MonoBehaviour
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
        EventSystem.OnLoginButtonPressed += OnLoginButtonPressed;
        EventSystem.OnSuccesfullLogin += OnSuccesfullLogin;

        EventSystem.OnPlayButtonPressed += OnPlayButtonPressed;
        EventSystem.OnLevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        EventSystem.OnLoginButtonPressed -= OnLoginButtonPressed;
        EventSystem.OnSuccesfullLogin -= OnSuccesfullLogin;

        EventSystem.OnPlayButtonPressed -= OnPlayButtonPressed;
        EventSystem.OnLevelLoaded -= OnLevelLoaded;

    }

    private void OnLoginButtonPressed()
    {
        Utility.EnablePanel(canvasGroup, true);
    }

    private void OnSuccesfullLogin()
    {
        Utility.EnablePanel(canvasGroup, false);
    }

    private void OnPlayButtonPressed()
    {
        Utility.EnablePanel(canvasGroup, true);
    }

    private void OnLevelLoaded()
    {
        Utility.EnablePanel(canvasGroup, false);
    }

}
