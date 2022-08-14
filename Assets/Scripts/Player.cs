using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Actor
{
    private static Player instance;
    public static Player Instance { get => instance; }

    [SerializeField] private TextMeshProUGUI textbox;

    [SerializeField] private Transform startPos;

    [SerializeField] private float maxInteractDist = 5f;
    public float MaxInteractDistSqr { get => maxInteractDist * maxInteractDist; }

    [SerializeField] private float moveTime = 1f;

    [SerializeField] private int startStrength = 1;

    public Transform Target { get; set; }
    public int CurrentStrength { get; set; }
    public bool IsDead { get; private set; }

    private Vector2 currVelocity;

    protected override void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        Target = startPos;
        CurrentStrength = startStrength;
        textbox.text = CurrentStrength.ToString();

        base.Awake();
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Target.position, ref currVelocity, moveTime);
    }

    public bool IsMoving()
    {
        return (transform.position - Target.position).sqrMagnitude > 0.01f;
    }

    public void Fight(Enemy enemy)
    {
        if (CurrentStrength >= enemy.Strength)
        {
            enemy.Explode();
            Destroy(enemy.gameObject);
            CurrentStrength += enemy.SubtractStrength ? -enemy.Strength : enemy.Strength;
            textbox.text = CurrentStrength.ToString();
        }
        else
        {
            Explode();
        }
    }

    public override void Explode()
    {
        base.Explode();

        IsDead = true;

        HUD.OnDied();
    }
}