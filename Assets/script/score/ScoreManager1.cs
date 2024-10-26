using UnityEngine;

public class ScoreManager1 : MonoBehaviour
{
    public static ScoreManager1 Instance { get; private set; } // Singleton Instance

    private bool isEnemyHit = false;
    private float enemyHitTime = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // オブジェクトをシーン間で保持
        }
        else
        {
            Destroy(gameObject); // 既存のインスタンスがあれば削除
        }
    }

    void Update()
    {
        if (isEnemyHit && Time.time - enemyHitTime > 20f)
        {
            isEnemyHit = false;
        }
    }

    public void OnEnemyHit()
    {
        isEnemyHit = true;
        enemyHitTime = Time.time;
        ScoreStorage.Instance.AddBonusHitCount();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isEnemyHit && Time.time - enemyHitTime <= 20f)
            {
                ScoreStorage.Instance.AddScore(150);
                isEnemyHit = false;
            }
            else
            {
                ScoreStorage.Instance.SubtractScore(50);
            }
        }

        if (other.CompareTag("BulletEnemy"))
        {
            ScoreStorage.Instance.SubtractScore(50);
        }
    }

    public int GetScore()
    {
        return ScoreStorage.Instance.GetScore();
    }

    public int GetBonusHitCount()
    {
        return ScoreStorage.Instance.GetBonusHitCount();
    }
}
