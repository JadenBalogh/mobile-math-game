using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] bodyParts;
    [SerializeField] private float minExplodeForce = 5f;
    [SerializeField] private float maxExplodeForce = 12f;
    [SerializeField] private float minExplodeTorque = 5f;
    [SerializeField] private float maxExplodeTorque = 12f;

    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Explode()
    {
        foreach (Rigidbody2D bodyPart in bodyParts)
        {
            Rigidbody2D part = Instantiate(bodyPart, transform.position, Quaternion.identity);
            float explodeForce = Random.Range(minExplodeForce, maxExplodeForce);
            float explodeTorque = Random.Range(minExplodeTorque, maxExplodeTorque);
            part.AddForce(Random.insideUnitCircle * explodeForce, ForceMode2D.Impulse);
            part.AddTorque(explodeTorque, ForceMode2D.Impulse);
        }

        spriteRenderer.enabled = false;
    }
}
