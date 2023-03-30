using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelView : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public List<BallAttributes> ballAttributes;
    public List<ShopItemView> shopItemViews;

    [SerializeField] private Button backButton;

    [SerializeField] private Transform shopContentParent;
    [SerializeField] private GameObject shopItemPrefab;

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
        EventSystem.OnInventoryRefreshed += OnInvetoryRefreshed;
    }


    private void OnDisable()
    {
        EventSystem.OnShopPanelOpened -= OnShopPanelOpen;
        EventSystem.OnInventoryRefreshed -= OnInvetoryRefreshed;
    }

    private void OnInvetoryRefreshed(IList<BallAttributes> obj)
    {
        ballAttributes = new List<BallAttributes>();
        ballAttributes = (List<BallAttributes>)obj;
        
        CreateShopItems();
    }

    private void CreateShopItems()
    {
        foreach (var item in shopItemViews)
        {
            Destroy(item.gameObject);
        }

        shopItemViews = new List<ShopItemView>();

        for (int i = 0; i < ballAttributes.Count; i++)
        {
            GameObject temp = Instantiate(shopItemPrefab, shopContentParent);
            shopItemViews.Add(temp.GetComponent<ShopItemView>());
        }

        RefrestBallShop();
    } 

    private void OnShopPanelOpen()
    {
        Utility.EnablePanel(canvasGroup, true);

        EventSystem.CallInventoryRefresh();

        RefrestBallShop();
    }

    private void RefrestBallShop()
    {
        for (int i = 0; i < ballAttributes.Count; i++)
        {
            shopItemViews[i].ballAttributes = ballAttributes[i];
            shopItemViews[i].UpdateShopItem();
        }
    }

    private void OnBackButtonClicked()
    {
        Utility.EnablePanel(canvasGroup, false);
        EventSystem.CallShopPanelClosed();
    }
}
