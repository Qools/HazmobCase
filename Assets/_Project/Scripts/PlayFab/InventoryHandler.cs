using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private int currency;

    [SerializeField] private List<BallAttributes> ballsCatalog;

    [SerializeField] private List<BallAttributes> unlockedBalls;

    private BallAttributes ballToBuy;

    private void OnEnable()
    {
        EventSystem.OnSuccesfullLogin += GetBallCatalog;
        EventSystem.OnSuccesfullLogin += GetCurrency;

        EventSystem.OnInventoryRefresh += GetInventory;

        EventSystem.OnBallCatalogRefresh += GetBallCatalog;

        EventSystem.OnBallSelected += OnBallSelected;

        EventSystem.OnBallBuy += OnBallBuy;
    }

    private void OnDisable()
    {
        EventSystem.OnSuccesfullLogin -= GetBallCatalog;
        EventSystem.OnSuccesfullLogin -= GetCurrency;

        EventSystem.OnInventoryRefresh -= GetInventory;

        EventSystem.OnBallCatalogRefresh -= GetBallCatalog;

        EventSystem.OnBallSelected -= OnBallSelected;
        EventSystem.OnBallBuy -= OnBallBuy;
    }

    private void GetBallCatalog()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogItemsSuccess, OnError);
    }


    private void OnGetCatalogItemsSuccess(GetCatalogItemsResult result)
    {
        ballsCatalog = new List<BallAttributes>();

        foreach (var item in result.Catalog)
        {
            var tempCustomData = JsonUtility.FromJson<BallAttributes>(item.CustomData);

            tempCustomData.ballID = item.ItemId;
            tempCustomData.ballName = item.DisplayName.ToString();
            tempCustomData.ballPrice = (int)item.VirtualCurrencyPrices[PlayerPrefKeys.gold];

            ballsCatalog.Add(tempCustomData);
        }

        Debug.Log("Catalog Items Loaded");
        
        GetInventory();
    }

    private void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnUserGetInvetorySuccess, OnError);
    }

    private void OnUserGetInvetorySuccess(GetUserInventoryResult result)
    {
        unlockedBalls = new List<BallAttributes>();

        foreach (var item in result.Inventory)
        {
            BallAttributes tempBall = new BallAttributes
            {
                ballID = item.ItemId
            };

            unlockedBalls.Add(tempBall);
        }
        
        for (int i = 0; i < ballsCatalog.Count; i++)
        {
            for (int j = 0; j < unlockedBalls.Count; j++)
            {
                if(ballsCatalog[i].ballID == unlockedBalls[j].ballID)
                {
                    ballsCatalog[i].isUnlocked = true;
                }

            }
        }
        EventSystem.CallBallCatalogRefreshed(ballsCatalog);
        Debug.Log("Invetory Loaded");
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log(error.Error);
    }

    private void GetCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetCurrencySuccess, OnError);
    }

    private void OnGetCurrencySuccess(GetUserInventoryResult result)
    {
        currency = result.VirtualCurrency[PlayerPrefKeys.gold];

        EventSystem.CallCurrencyRefresh(currency);
    }

    private void OnBallSelected(BallAttributes ball)
    {
        foreach (var item in ballsCatalog)
        {
            if (ball.ballID == item.ballID)
            {
                item.isSelected = true;
                return;
            }

            item.isSelected = false;
        }
    }

    public void AddCurrency(int _value)
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = PlayerPrefKeys.gold,
            Amount = _value
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCurrencySuccess, OnError);
    }

    private void OnAddCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        currency = result.Balance;
        EventSystem.CallCurrencyRefresh(currency);
        Debug.Log("Currency " + currency);
    }

    private void OnBallBuy(BallAttributes ball)
    {
        ballToBuy = ball;

        if (ballToBuy.ballPrice <= currency)
        {
            var request = new PurchaseItemRequest
            {
                CatalogVersion = "0.1",
                ItemId = ballToBuy.ballID,
                Price = ballToBuy.ballPrice,
                VirtualCurrency = PlayerPrefKeys.gold,
            };

            PlayFabClientAPI.PurchaseItem(request, OnPurchaseItemSuccess, OnPurchaseItemError);
        }
    }

    private void OnPurchaseItemSuccess(PurchaseItemResult result)
    {
        Debug.Log(result.Items[0].DisplayName);
        ballToBuy.isUnlocked = true;

        GetCurrency();

        EventSystem.CallInventoryRefresh();
    }

    private void OnPurchaseItemError(PlayFabError error)
    {
        Debug.Log(error);
    }
}
