using UnityEngine;

namespace PachinkoTest
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PegController : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 여기에 사운드나 비주얼 효과를 추가할 수 있습니다.
            if (collision.gameObject.CompareTag("Player")) // 공의 태그가 "Player"라고 가정
            {
                 Debug.Log("공이 못에 부딪혔습니다!");
            }
        }
    }
}