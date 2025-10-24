using System;
using System.Collections;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
#if UNITY_ANDROID
    private Gyroscope mGyroscope;
    private float mMagnitude;
#elif UNITY_EDITOR
    private readonly float m_GravityScale = 9.81f * 1.5f;
#endif
    
    private void Awake()
    {
#if UNITY_ANDROID
        // if (SystemInfo.supportsGyroscope) 
        // {
        //     Input.gyro.enabled = true;
        //     mGyroscope = Input.gyro;
        // }
        // else 
        // {
        //     Debug.Log("이 장치는 자이로스코프를 지원하지 않습니다.");
        //     Input.gyro.enabled = false;
        //     gameObject.SetActive(false);
        // }
        
        Input.gyro.enabled = true;
        mGyroscope = Input.gyro;
        StartCoroutine(Startset());
#elif UNITY_EDITOR
        Debug.Log("PC 컨트롤 모드입니다. 화살표 키를 사용하여 중력을 조작하세요");
#endif
    }
    
    private void FixedUpdate()
    {
        Physics2D.gravity = Vector2.zero;
        
        var playerCharacter = InGameHolder.Instance.playerCharacter;

        if (playerCharacter != null && playerCharacter.Rb != null)
        {
#if UNITY_ANDROID
            Vector2 gravityForce = mGyroscope.gravity * (9.81f * 1.5f);
            playerCharacter.Rb.AddForce(gravityForce * playerCharacter.Rb.mass);

            // 플레이어 회전 로직 추가
            Vector2 lookDirection = -gravityForce.normalized; // 중력의 반대 방향을 바라보도록
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg; // 벡터의 각도 계산
            // z축 0일 때 왼쪽을 바라보는게 기본 상태이므로, 180도 오프셋을 적용하여 왼쪽을 바라보게 합니다.
            // Unity 2D에서 transform.right가 (1,0)이므로, 왼쪽을 바라보려면 -90도 (또는 270도)가 필요할 수 있습니다.
            // 여기서는 Atan2가 반환하는 각도에 180도를 더하여 왼쪽을 바라보게 합니다.
            playerCharacter.transform.rotation = Quaternion.Euler(0, 0, angle + 180f); // 180도 오프셋 적용
            
            mMagnitude = mGyroscope.userAcceleration.magnitude;
            if (mMagnitude > 0.5f)
            {
                InGameEventHandler.Instance.GyroShakeHandler?.Invoke(mMagnitude);
                playerCharacter.Rb.AddForce(mGyroscope.userAcceleration * 15f, ForceMode2D.Impulse);
            }
#elif UNITY_EDITOR
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var gravityDirection = new Vector2(horizontalInput, verticalInput);
            var gravityForce = gravityDirection * m_GravityScale;
            playerCharacter.Rb.AddForce(gravityForce * playerCharacter.Rb.mass);

            // 플레이어 회전 로직 추가 (에디터)
            if (gravityDirection.magnitude > 0.1f) // 입력이 있을 때만 회전
            {
                Vector2 lookDirection = -gravityDirection.normalized; // 중력의 반대 방향을 바라보도록
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                playerCharacter.transform.rotation = Quaternion.Euler(0, 0, angle + 180f); // 180도 오프셋 적용
            }
#endif
        }
    }

#if UNITY_ANDROID
    private IEnumerator Startset()
    {
        while (true)
        {
            Debug.Log($"x: {mGyroscope.gravity.x}, y: {mGyroscope.gravity.y}, z: {mGyroscope.gravity.z}");
            yield return new WaitForSeconds(1);
        }
    }
#endif
}
