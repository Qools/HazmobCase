using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
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

    private static int HexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex, 16);

        return dec;
    }

    private static float HexToFloatNormalized(string hex)
    {
        return HexToDec(hex) / 255f;
    }

    public static Color GetColorFromString(string hexString)
    {
        float red = HexToFloatNormalized(hexString.Substring(0, 2));
        float green = HexToFloatNormalized(hexString.Substring(2, 2));
        float blue = HexToFloatNormalized(hexString.Substring(4, 2));

        return new Color(red, green, blue);
    }
}
