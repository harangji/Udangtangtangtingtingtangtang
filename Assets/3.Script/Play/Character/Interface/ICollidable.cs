using System;

public enum EColliderCamp
{
    AllyCamp,
    EnemyCamp,
}

public interface ICollidable
{
    public EColliderCamp ColliderCamp { get; }
    public void OnCollide(ICharacter target);
}