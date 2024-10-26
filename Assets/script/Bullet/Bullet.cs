using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 20f; // フリーズの持続時間

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Army enemyScript = other.GetComponent<Army>();
            if (enemyScript != null)
            {
                StartCoroutine(FreezeEnemy(enemyScript));
            }

            // ScoreManager1 にスコアの更新を通知
            if (ScoreManager1.Instance != null)
            {
                ScoreManager1.Instance.OnEnemyHit();
            }

            Destroy(gameObject); // 弾を破棄
        }
    }

    private IEnumerator FreezeEnemy(Army enemy)
    {
        enemy.isFrozen = true;
        yield return new WaitForSeconds(freezeDuration);
        enemy.isFrozen = false;
    }
}
