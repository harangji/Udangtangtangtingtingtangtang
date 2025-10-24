using System;
using UnityEngine;

public class ShowAmountTextEventArgs : EventArgs
{
    public int Amount { get; set; } = 0; // Damage -> Amount로 변경
    public Vector2 HitPosition { get; set; } = Vector2.zero;
    public Color Color { get; set; } = Color.red;
}