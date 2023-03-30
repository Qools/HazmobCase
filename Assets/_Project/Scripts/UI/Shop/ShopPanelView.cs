using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanelView : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public List<BallAttributes> ballCatalog;
    public List<ShopItemView> shopItemViews;

    [SerializeField] private Button backButton;

    [SerializeField] private Transform shopContentParent;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private TextMeshProUGUI currencyText;

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

        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnEnable()
    {
        EventSystem.OnShopPanelOpened += OnShopPanelOpen;

        EventSystem.OnCurrencyRefresh += RefreshCurrencyText;

        EventSystem.OnBallCatalogRefreshed += OnBallCatalogRefreshed;
    }


    private void OnDisable()
    {
        EventSystem.OnShopPanelOpened -= OnShopPanelOpen;

        EventSystem.OnCurrencyRefresh -= RefreshCurrencyText;

        EventSystem.OnBallCatalogRefreshed -= OnBallCatalogRefreshed;
    }

    private void OnBallCatalogRefreshed(IList<BallAttributes> obj)
    {
        ballCatalog = new List<BallAttributes>();
        ballCatalog = (List<BallAttributes>)obj;
        
        CreateShopItems();
    }

    private void CreateShopItems()
    {
        foreach (var item in shopItemViews)
        {
            Destroy(item.gameObject);
        }

        shopItemViews = new List<ShopItemView>();

        for (int i = 0; i < ballCatalog.Count; i++)
        {
            GameObject temp = Instantiate(shopItemPrefab, shopContentParent);

            if (temp.TryGetComponent(out ShopItemView shopItemView))
            {
                shopItemViews.Add(shopItemView);
            }
        }

        RefrestBallShop();
    } 

    private void OnShopPanelOpen()
    {
        Utility.EnablePanel(canvasGroup, true);

        EventSystem.CallBallCatalogRefresh();

        RefrestBallShop();
    }

    private void RefrestBallShop()
    {
        for (int i = 0; i < ballCatalog.Count; i++)
        {
            shopItemViews[i].ballAttributes = ballCatalog[i];
            shopItemViews[i].UpdateShopItem();
        }
    }

    private void OnBackButtonClicked()
    {
        Utility.EnablePanel(canvasGroup, false);
        EventSystem.CallShopPanelClosed();
    }

    private void RefreshCurrencyText(int currency)
    {
        currencyText.text = currency.ToString() + " " + PlayerPrefKeys.gold;
    }
}
