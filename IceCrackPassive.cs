using UnityEngine;

public class IceCrackPassive : MonoBehaviour
{
    public GameObject iceCrackPrefab; // 얼음 균열 프리팹
    public float detectRadius = 3f;    // 발동 거리
    public float spawnCooldown = 1f;   // 생성 쿨타임

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        Collider2D enemy = Physics2D.OverlapCircle(
            transform.position,
            detectRadius,
            LayerMask.GetMask("Enemy")
        );

        if (enemy != null && timer >= spawnCooldown)
        {
            SpawnIceCrack(enemy.transform.position);
            timer = 0f;
        }
    }

    void SpawnIceCrack(Vector2 position)
    {
        Instantiate(iceCrackPrefab, position, Quaternion.identity);
    }

    // 에디터에서 범위 시각화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
