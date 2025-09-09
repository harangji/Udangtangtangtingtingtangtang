using UnityEngine;

public interface ICollidable
{
    public EColliderCamp Camp { get; }
    public CircleCollider2D Col { get; }
    public Rigidbody2D Rb { get; }
    public void OnCollide(Vector3 position);
}