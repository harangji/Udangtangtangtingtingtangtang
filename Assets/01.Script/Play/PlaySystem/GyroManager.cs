using System.Collections;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
#if UNITY_ANDROID
    
    private Gyroscope mGyroscope;
    
    private void Awake()
    {
        // if (SystemInfo.supportsGyroscope) {
        //     Input.gyro.enabled = true;
        //     gyroscope = Input.gyro;
        // } else {
        //     Debug.Log("이 장치는 자이로스코프를 지원하지 않습니다.");
        //     Input.gyro.enabled = false;
        //     gameObject.SetActive(false);
        // }
        
        Input.gyro.enabled = true;
        mGyroscope = Input.gyro;
        StartCoroutine(Startset());
    }
    
    private void FixedUpdate()
    {
        Physics2D.gravity = mGyroscope.gravity * (9.81f * 1.5f);
        
        if (mGyroscope.userAcceleration.magnitude > 0.5f)
        {
            foreach (var character in InGameHolder.Instance.GetCharacters())
            {
                character.Interface.Collidable.Rb.AddForce(Input.gyro.userAcceleration * 20f, ForceMode2D.Impulse);
            }
        }
    }

    private IEnumerator Startset()
    {
        while (true)
        {
            Debug.Log($"x: {mGyroscope.gravity.x}, y: {mGyroscope.gravity.y}, z: {mGyroscope.gravity.z}");
            yield return new WaitForSeconds(1);
        }
    }
    
    // private void FixedUpdate()
    // {
    //     Debug.Log(Input.acceleration.x);
    //     Debug.Log(Input.acceleration.y);
    //     float gx = Input.acceleration.x * 9.81f;
    //     float gy = Input.acceleration.y * 9.81f;
    //
    //     Physics2D.gravity = new Vector2(gx, gy);
    //     
    //     Debug.Log($"x: {gyroscope.gravity.x}, y: {gyroscope.gravity.y}, z: {gyroscope.gravity.z}");
    // }
#endif
}
