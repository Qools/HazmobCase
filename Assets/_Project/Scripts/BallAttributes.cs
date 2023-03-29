using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBall", menuName = "NewBall")]
public class BallAttributes : ScriptableObject
{
    public bool isUnlocked = false;
    public bool isSelected = false;

    public string ballName;
    public float ballSize;
    public float ballWeight;
    public Color ballColor;
    public float ballPrice;
}
