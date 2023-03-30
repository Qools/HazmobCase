using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemView : MonoBehaviour
{
    public BallAttributes ballAttributes;

    [SerializeField] private TextMeshProUGUI ballNameText;
    [SerializeField] private TextMeshProUGUI ballPriceText;
    [SerializeField] private Image ballImage;

    [SerializeField] private Button buyButton;
    [SerializeField] private Button selectButton;

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    public void UpdateShopItem()
    {
        ballNameText.text = ballAttributes.ballName;
        ballPriceText.text = ballAttributes.ballPrice.ToString() + " " + PlayerPrefKeys.gold;
        ballImage.color = Utility.GetColorFromString(ballAttributes.ballColor);

        RefreshButtons();
    }

    private void OnSelectButtonClicked()
    {
        EventSystem.CallBallSelected(ballAttributes);

        RefreshButtons();
    }

    private void OnBuyButtonClicked()
    {
        EventSystem.CallBallBuy(ballAttributes);
        RefreshButtons();
    }

    private void RefreshButtons()
    {
        buyButton.gameObject.SetActive(!ballAttributes.isUnlocked);
        selectButton.gameObject.SetActive(ballAttributes.isUnlocked);
    }
}
