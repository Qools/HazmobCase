using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public Button playButton;
    public Button shopButton;


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

        playButton.onClick.AddListener(OnPlayButtonClicked);
        shopButton.onClick.AddListener(OnShopButtonClicked);
    }

    private void OnEnable()
    {
        EventSystem.OnSuccesfullLogin += OnSuccesfullLogin;
        EventSystem.OnShopPanelClosed += OnSuccesfullLogin;
    }

    private void OnDisable()
    {
        EventSystem.OnSuccesfullLogin -= OnSuccesfullLogin;
        EventSystem.OnShopPanelClosed -= OnSuccesfullLogin;
    }

    private void OnSuccesfullLogin()
    {
        Utility.EnablePanel(canvasGroup, true);
    }

    public void OnPlayButtonClicked()
    {
        Utility.EnablePanel(canvasGroup, false);

        EventSystem.CallGameStarted();
    }

    public void OnShopButtonClicked()
    {
        Utility.EnablePanel(canvasGroup, false);

        EventSystem.CallShopPanelOpened();
    }
}
