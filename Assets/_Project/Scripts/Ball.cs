using System;
using UnityEngine;

[System.Serializable]
public class Ball
{
    public bool isUnlocked = false;
    public bool isSelected = false;

    public string ballID;
    public string ballName;
    public float ballSize;
    public float ballWeight;
    public string ballColor;
    public int ballPrice;
}
