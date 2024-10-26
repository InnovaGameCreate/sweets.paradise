using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float freezeDuration = 20f; // �t���[�Y�̎�������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Army enemyScript = other.GetComponent<Army>();
            if (enemyScript != null)
            {
                StartCoroutine(FreezeEnemy(enemyScript));
            }

            // ScoreManager1 �ɃX�R�A�̍X�V��ʒm
            if (ScoreManager1.Instance != null)
            {
                ScoreManager1.Instance.OnEnemyHit();
            }

            Destroy(gameObject); // �e��j��
        }
    }

    private IEnumerator FreezeEnemy(Army enemy)
    {
        enemy.isFrozen = true;
        yield return new WaitForSeconds(freezeDuration);
        enemy.isFrozen = false;
    }
}
