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
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private float combatDelay = 0.5f;

    private bool PlayerArrived { get => (Player.Instance.transform.position - playerPos.transform.position).sqrMagnitude < 0.1f; }

    public int StrengthMult { get; set; }

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
        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        enemy = Instantiate(enemyPrefab, enemyPos.position, Quaternion.identity);
        enemy.Strength = enemy.Strength * StrengthMult;
    }

    private void Update()
    {
        if (explored) return;

        if (PlayerArrived && !explored)
        {
            StartCoroutine(DelayCombat());
            explored = true;
        }

        if (IsUsable())
        {
            spriteRenderer.color = isHovered ? hoverColor : usableColor;
        }
        else
        {
            spriteRenderer.color = normalColor;
        }
    }

    private IEnumerator DelayCombat()
    {
        yield return new WaitForSeconds(combatDelay);
        Player.Instance.Fight(enemy);
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
