using System;
using UnityEngine;

public class ShowDamageTextEventArgs : EventArgs
{
    public int Damage { get; set; } = 0;
    public Vector2 HitPosition { get; set; } = Vector2.zero;
    public Color Color { get; set; } = Color.red;
}

public class CollideEventArgs : EventArgs
{
    public CharacterBase Sender { get; set; }
    public CharacterBase Receiver { get; set; }
}