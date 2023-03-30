using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSystem
{
    public static Action OnSuccesfullLogin;
    public static void CallSuccesfullLogin() => OnSuccesfullLogin?.Invoke();

    public static Action OnGameStarted;
    public static void CallGameStarted() => OnGameStarted?.Invoke();

    public static Action OnGameOver;
    public static void CallGameOver() => OnGameOver?.Invoke();

    public static Action OnShopPanelOpened;
    public static void CallShopPanelOpened() => OnShopPanelOpened?.Invoke();
    
    public static Action OnShopPanelClosed;
    public static void CallShopPanelClosed() => OnShopPanelClosed?.Invoke();

    public static Action OnBallCatalogRefresh;
    public static void CallBallCatalogRefresh() => OnBallCatalogRefresh?.Invoke();

    public static Action<IList<BallAttributes>> OnBallCatalogRefreshed;
    public static void CallBallCatalogRefreshed(List<BallAttributes> ballCatalog) { OnBallCatalogRefreshed?.Invoke(ballCatalog); }

    public static Action OnInventoryRefresh;
    public static void CallInventoryRefresh() => OnInventoryRefresh?.Invoke();

    public static Action<BallAttributes> OnBallSelected;
    public static void CallBallSelected(BallAttributes selectedBall) => OnBallSelected?.Invoke(selectedBall);

    public static Action<BallAttributes> OnBallBuy;
    public static void CallBallBuy(BallAttributes ballToBuy) => OnBallBuy?.Invoke(ballToBuy);

    public static Action<int> OnCurrencyRefresh;
    public static void CallCurrencyRefresh(int currency) => OnCurrencyRefresh?.Invoke(currency);
}
