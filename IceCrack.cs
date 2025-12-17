using UnityEngine;
using System.Collections;

public class IceCrack : MonoBehaviour
{
    public float damage = 10f;
    public float slowAmount = 0.5f;
    public float slowDuration = 1f;
    public float freezeDelay = 0.25f;
    public float freezeDuration = 1.5f;

    private bool frozenTriggered = false;

    void Start()
    {
        StartCoroutine(FreezeAfterDelay());
        Destroy(gameObject, 2f); // 균열 자동 제거
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.ApplySlow(slowAmount, slowDuration);
            }
        }
    }

    IEnumerator FreezeAfterDelay()
    {
        yield return new WaitForSeconds(freezeDelay);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            0.5f,
            LayerMask.GetMask("Enemy")
        );

        foreach (Collider2D col in enemies)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Freeze(freezeDuration);
            }
        }

        frozenTriggered = true;
    }
}
