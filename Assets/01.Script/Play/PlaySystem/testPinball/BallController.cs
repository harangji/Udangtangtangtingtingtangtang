using UnityEngine;

namespace PachinkoTest
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class BallController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float minVelocity = 0.1f; // The velocity below which we consider the ball 'stuck'

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // This is called almost every frame while the ball is touching another collider
        private void OnCollisionStay2D(Collision2D collision)
        {
            // Check if the ball has nearly stopped while in contact with something
            if (rb.linearVelocity.magnitude < minVelocity)
            {
                // Give it a tiny random horizontal push to get it unstuck
                float randomNudge = Random.Range(-0.1f, 0.1f);
                // Ensure the nudge is not zero
                if (Mathf.Approximately(randomNudge, 0)) randomNudge = 0.1f;

                rb.AddForce(new Vector2(randomNudge, 0), ForceMode2D.Impulse);
                Debug.Log("Ball was stuck, nudging it!");
            }
        }

        // Destroy the ball if it falls out of bounds to prevent memory leaks
        private void OnBecameInvisible()
        {
            Destroy(gameObject, 1f); // Delay to ensure it's fully off-screen
        }
    }
}
