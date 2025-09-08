using UnityEngine;

public class GyroManager : MonoBehaviour
{
#if UNITY_ANDROID
    private void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        Vector3 g = Input.gyro.gravity;
        
        Physics.gravity = g * 9.81f; 
    }
    
    // void FixedUpdate()
    // {
    //     Debug.Log(Input.acceleration.x);
    //     Debug.Log(Input.acceleration.y);
    //     float gx = Input.acceleration.x * 9.81f;
    //     float gy = Input.acceleration.y * 9.81f;
    //
    //     Physics2D.gravity = new Vector2(gx, gy);
    // }
#endif
}
