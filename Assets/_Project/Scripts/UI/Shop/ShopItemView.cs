using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemView : MonoBehaviour
{
    public BallAttributes ballAttributes;

    public TextMeshProUGUI ballNameText;
    public TextMeshProUGUI ballPriceText;
    public Image ballImage;

    public Button buyButton;
    public Button selectButton;

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    public void UpdateShopItem()
    {
        ballNameText.text = ballAttributes.ballName;
        ballPriceText.text = ballAttributes.ballPrice.ToString() + " " + "Gold";
        ballImage.color = Utility.GetColorFromString(ballAttributes.ballColor);

        RefreshButtons();
    }

    private void OnSelectButtonClicked()
    {
        ballAttributes.isSelected = true;

        RefreshButtons();
    }

    private void OnBuyButtonClicked()
    {
        ballAttributes.isUnlocked = true;

        RefreshButtons();
    }

    private void RefreshButtons()
    {
        buyButton.gameObject.SetActive(!ballAttributes.isUnlocked);
        selectButton.gameObject.SetActive(ballAttributes.isUnlocked);
    }
}
