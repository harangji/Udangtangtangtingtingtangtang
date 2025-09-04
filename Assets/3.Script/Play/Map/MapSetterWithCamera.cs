using System;
using UnityEngine;

public class MapSetterWithCamera : MonoBehaviour
{
    public EdgeCollider2D edge;
    private Camera targetCamera;
    
    private void Awake()
    {
        targetCamera = InGameHolder.Instance.mainCamera;
        UpdateCollider();
    }

    private void EscapeCatch()
    {
        
    }
    
    private void UpdateCollider()
    {
        if (targetCamera == null) return;
        
        float height = targetCamera.orthographicSize * 2f;
        float width = height * targetCamera.aspect;

        float left   = -width * 0.5f;
        float right  =  width * 0.5f;
        float bottom = -height * 0.5f;
        float top    =  height * 0.5f;
        
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(left, bottom);
        points[1] = new Vector2(left, top);
        points[2] = new Vector2(right, top);
        points[3] = new Vector2(right, bottom);
        points[4] = new Vector2(left, bottom);

        edge.points = points;
        transform.position = targetCamera.transform.position;
    }
}