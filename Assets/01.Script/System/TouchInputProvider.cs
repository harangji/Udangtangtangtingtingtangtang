using UnityEngine;
using System;

public class TouchInputProvider : SingletonBase<TouchInputProvider>
{
    protected override bool dontDestroyOnLoad { get; set; } = true;
    
    public event Action<Vector2> OnTouchEnded;
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Vector3 worldPos = InGameHolder.Instance.mainCamera.ScreenToWorldPoint(touch.position);
                OnTouchEnded?.Invoke(new Vector2(worldPos.x, worldPos.y));
            }
        }
    }
}
