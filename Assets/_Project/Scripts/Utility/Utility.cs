using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void EnablePanel(CanvasGroup _canvasGroup, bool _value)
    {
        _canvasGroup.interactable = _value;
        _canvasGroup.blocksRaycasts = _value;

        if (_value)
        {
            _canvasGroup.alpha = 1f;

            return;

        }

        _canvasGroup.alpha = 0f;
    }
}
