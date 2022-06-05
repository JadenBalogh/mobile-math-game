using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance { get => instance; }

    [SerializeField] private Transform startPos;

    [SerializeField] private float maxInteractDist = 5f;
    public float MaxInteractDistSqr { get => maxInteractDist * maxInteractDist; }

    [SerializeField] private float moveTime = 1f;

    public Transform Target { get; set; }

    private Vector2 currVelocity;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Target = startPos;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Target.position, ref currVelocity, moveTime);
    }

    public bool IsMoving()
    {
        return (transform.position - Target.position).sqrMagnitude > 0.01f;
    }
}
