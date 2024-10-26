using System.Collections;
using UnityEngine;

public class Army : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float enemyLifeTime;
    [SerializeField] private Transform player;
    [SerializeField] private float stopDistance = 10f;  // プレイヤーとの停止距離
    [SerializeField] private float fleeDistance = 5f;   // プレイヤーとの逃走距離
    [SerializeField] private float approachDistance = 15f;  // プレイヤーに向かって進む距離
    [SerializeField] private GameObject enemyBulletPrefab; // 弾のPrefab
    [SerializeField] private float bulletSpeed = 10f;   // 弾の速度
    [SerializeField] private float shootCooldown = 2f;   // 弾発射のクールダウン
    public bool isFrozen = false;  // 凍結状態
    private bool canShoot = true;  // 弾発射可能かどうか
    private Vector3 randomDestination;  // ランダムな目的地
    private float randomMoveCooldown = 5f;  // ランダム移動を更新する間隔
    private float randomMoveTimer;

    [SerializeField] private float maxRange = 30f;  // エネミーが移動できる最大距離
    [SerializeField] private float minRange = 5f;   // エネミーが移動できる最小距離

    private ScoreManager1 scoreManager; // スコアを管理するScoreManager

    // 効果音を再生するためのAudioClipとAudioSource
    [SerializeField] private AudioClip hitSE; // バレットに当たったときの効果音
    [SerializeField] private AudioClip shootSE; // 弾発射時の効果音
    private AudioSource audioSource; // 効果音を再生するAudioSource

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // ScoreManagerの参照を取得
        scoreManager = FindObjectOfType<ScoreManager1>();

        // AudioSourceを追加して取得
        audioSource = gameObject.AddComponent<AudioSource>();

        // 最初のランダムな目的地を設定
        SetRandomDestination();
    }

    void Update()
    {
        if (!isFrozen && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // プレイヤーに近づきすぎていない場合にランダム移動
            if (distanceToPlayer > approachDistance)
            {
                RandomMove();
            }
            else if (distanceToPlayer <= fleeDistance)
            {
                // プレイヤーから逃げる
                Vector3 direction = (transform.position - player.position).normalized;
                MoveAndClampPosition(direction);
            }
            else if (distanceToPlayer <= stopDistance)
            {
                // 停止距離以内なら攻撃
                if (canShoot)
                {
                    StartCoroutine(ShootAtPlayer());
                }
            }
            else
            {
                // プレイヤーに向かって進む
                Vector3 direction = (player.position - transform.position).normalized;
                MoveAndClampPosition(direction);
            }
        }

        enemyLifeTime -= Time.deltaTime;
        if (enemyLifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    // ランダム移動の処理
    private void RandomMove()
    {
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f)
        {
            SetRandomDestination();
        }

        // ランダムな目的地に向かって移動
        Vector3 direction = (randomDestination - transform.position).normalized;
        MoveAndClampPosition(direction);

        // 目的地に到着したら、新しい目的地を設定
        if (Vector3.Distance(transform.position, randomDestination) < 1f)
        {
            SetRandomDestination();
        }
    }

    // ランダムな目的地を設定する
    private void SetRandomDestination()
    {
        float randomDistance = Random.Range(minRange, maxRange); // 最小範囲と最大範囲の間の距離
        float randomAngle = Random.Range(0f, 2 * Mathf.PI); // ランダムな角度

        // 極座標から新しい目的地のX, Z座標を計算
        randomDestination = new Vector3(
            randomDistance * Mathf.Cos(randomAngle),
            transform.position.y, // Y軸は固定
            randomDistance * Mathf.Sin(randomAngle)
        );

        randomMoveTimer = randomMoveCooldown;

        ClampPosition(ref randomDestination); // 範囲外に出ないように目的地を調整
    }

    // エネミーの移動範囲を制限する処理
    private void ClampPosition(ref Vector3 position)
    {
        float distanceFromCenter = Vector3.Distance(Vector3.zero, position);

        // 最大範囲を超えていれば、最大範囲内に制限
        if (distanceFromCenter > maxRange)
        {
            position = position.normalized * maxRange;
        }

        // 最小範囲より内側であれば、最小範囲に制限
        if (distanceFromCenter < minRange)
        {
            position = position.normalized * minRange;
        }
    }

    // エネミーの移動と範囲制限
    private void MoveAndClampPosition(Vector3 direction)
    {
        transform.Translate(direction * enemySpeed * Time.deltaTime, Space.World);

        // エネミーの現在位置を範囲内に制限する
        Vector3 clampedPosition = transform.position;
        ClampPosition(ref clampedPosition);
        transform.position = clampedPosition;
    }

    private IEnumerator ShootAtPlayer()
    {
        canShoot = false; // 弾発射クールダウン開始

        // 弾の生成と発射
        Quaternion rotation = Quaternion.Euler(-90, 0, 0);
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        // 弾発射時の効果音を再生
        PlayShootSE();

        yield return new WaitForSeconds(shootCooldown); // クールダウンを待つ
        canShoot = true; // 次の弾発射可能
    }

    // エネミーがプレイヤーの弾に当たった場合の処理
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 効果音を再生
            PlayHitSE();

            Destroy(gameObject);
        }
    }

    // 発射時の効果音を再生するメソッド
    private void PlayShootSE()
    {
        if (shootSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSE); // 発射時の効果音を一度再生
        }
    }

    // 被弾時の効果音を再生するメソッド
    private void PlayHitSE()
    {
        if (hitSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSE); // 被弾時の効果音を一度再生
        }
    }
}
