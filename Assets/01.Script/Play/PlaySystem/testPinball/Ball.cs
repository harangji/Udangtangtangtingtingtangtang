using UnityEngine;

// 공의 상태를 관리하는 스크립트입니다.
public class Ball : MonoBehaviour
{
    // 공이 이미 점수를 획득했는지 여부를 나타냅니다.
    // 한 번 점수를 내면 다시 점수를 낼 수 없도록 하는 데 사용됩니다.
    public bool hasScored = false;
}