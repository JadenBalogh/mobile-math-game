using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color usableColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform enemyPos;
    [SerializeField] private Enemy enemyPrefab;

    private Enemy enemy = null;
    private bool isHovered = false;
    private bool explored = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemy = Instantiate(enemyPrefab, enemyPos.position, Quaternion.identity);
    }

    private void Update()
    {
        if (explored) return;

        if (IsUsable())
        {
            spriteRenderer.color = isHovered ? hoverColor : usableColor;
        }
        else
        {
            spriteRenderer.color = normalColor;
        }
    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

    private void OnMouseDown()
    {
        if (!IsUsable()) return;

        Player.Instance.Target = playerPos;
    }

    private bool IsUsable()
    {
        Player player = Player.Instance;

        bool roomHasPlayer = player.Target == playerPos;
        bool roomHasEnemy = enemy != null;

        float distSqr = (transform.position - player.transform.position).sqrMagnitude;
        bool inRange = distSqr <= player.MaxInteractDistSqr;

        float xDist = player.transform.position.x - transform.position.x;
        bool isPast = xDist >= -0.1f;

        return !roomHasPlayer && roomHasEnemy && inRange && !isPast && !player.IsMoving();
    }
}
