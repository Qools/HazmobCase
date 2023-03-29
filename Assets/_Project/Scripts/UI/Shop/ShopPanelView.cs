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

        CreateShopItems();
    }

    private void OnEnable()
    {
        EventSystem.OnShopPanelOpened += OnShopPanelOpen;
    }

    private void OnDisable()
    {
        EventSystem.OnShopPanelOpened -= OnShopPanelOpen;
    }

    private void CreateShopItems()
    {
        for (int i = 0; i < ballAttributes.Count; i++)
        {
            GameObject temp = Instantiate(shopItemPrefab, shopContentParent);
            shopItemViews.Add(temp.GetComponent<ShopItemView>());
        }
    } 

    private void OnShopPanelOpen()
    {
        Utility.EnablePanel(canvasGroup, true);

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
