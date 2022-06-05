using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveTime;

    private Vector2 currVelocity;

    private void Update()
    {
        Vector3 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref currVelocity, moveTime);
        transform.position = targetPos + Vector3.forward * transform.position.z;
    }
}
