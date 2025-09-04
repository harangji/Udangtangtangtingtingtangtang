using System;
using DG.Tweening;
using UnityEngine;

public class TestMapRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 1));
    }
}
