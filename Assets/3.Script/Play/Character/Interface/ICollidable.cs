
using UnityEngine;

public interface ICollidable
{
    public EColliderCamp ColliderCamp { get; }
    public float InitRadius { get; }
    public void OnCollide(CombatEvent combatEvent);
}

public interface IDamageable
{
    public CircleCollider2D Col { get; }
    public Rigidbody2D Rb { get; }
    public void TakeDamage(int damage);
}