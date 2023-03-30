using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class InventoryHandler : MonoBehaviour
{
    private void GetBallAttributes()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogItemsSuccess, OnGetCatalogItemsError);
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnUserGetInvetorySuccess, OnUserGetInventoryError);
    }

    private void OnGetCatalogItemsSuccess(GetCatalogItemsResult result)
    {
        List<BallAttributes> ballAttributes = new List<BallAttributes>();

        foreach (var item in result.Catalog)
        {
            var tempCustomData = JsonUtility.FromJson<BallAttributes>(item.CustomData);

            tempCustomData.ballName = item.DisplayName.ToString();
            tempCustomData.ballPrice = item.VirtualCurrencyPrices["GC"];

            ballAttributes.Add(tempCustomData);
        }

        Debug.Log("Catalog Items Loaded");
        EventSystem.CallInventoryRefreshed(ballAttributes);
    }

    private void OnGetCatalogItemsError(PlayFabError error)
    {
        Debug.Log(error.Error);
    }

    private void OnUserGetInvetorySuccess(GetUserInventoryResult result)
    {
        Debug.Log(result.VirtualCurrency["GC"]);
    }

    private void OnUserGetInventoryError(PlayFabError error)
    {
        Debug.Log(error.Error);
    }

    private void OnEnable()
    {
        EventSystem.OnSuccesfullLogin += GetBallAttributes;
        EventSystem.OnInventoryRefresh += GetBallAttributes;
    }

    private void OnDisable()
    {
        EventSystem.OnSuccesfullLogin -= GetBallAttributes;
        EventSystem.OnInventoryRefresh -= GetBallAttributes;
    }
}
