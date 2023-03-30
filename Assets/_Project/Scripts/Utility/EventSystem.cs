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

    public static Action OnInventoryRefresh;
    public static void CallInventoryRefresh() => OnInventoryRefresh?.Invoke();

    public static Action<IList<BallAttributes>> OnInventoryRefreshed;
    public static void CallInventoryRefreshed(List<BallAttributes> ballAttributes) { OnInventoryRefreshed?.Invoke(ballAttributes); }
}
