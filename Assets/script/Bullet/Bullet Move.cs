using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Text bulletCountText; // UI テキスト
    [SerializeField] private float fieldRadius = 10f; // ステージの半径（最大範囲）
    [SerializeField] private float minRadius = 2f;   // ステージの最小範囲
    [SerializeField] private float autoFireInterval = 0.1f; // オート射撃の間隔
    [SerializeField] private int maxBullets = 10; // 弾の最大数
    [SerializeField] private AudioClip shootSE; // 発射時の効果音を設定
    private int bulletCount = 0;
    private float nextShootTime = 0;
    private Vector3 stageCenter = Vector3.zero; // ステージの中心
    private AudioSource audioSource; // 効果音を再生するAudioSource

    private bool isFiring = false; // オート射撃のフラグ

    void Start()
    {
        // AudioSourceを取得
        audioSource = gameObject.AddComponent<AudioSource>();

        UpdateBulletCountText(); // テキストの更新
    }

    void Update()
    {
        // スペースキーが押されたら発射
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextShootTime)
        {
            if (bulletCount > 0)
            {
                ShootBullet();
                bulletCount--;
                UpdateBulletCountText(); // 弾数を更新
                nextShootTime = Time.time + 1f;
            }
        }

        // オート射撃の処理（コメントアウト中）
        /*if (Input.GetKey(KeyCode.Mouse0) && bulletCount > 0 && !isFiring)
        {
            StartCoroutine(AutoFire());
        }*/
    }

    // オート射撃用のコルーチン
    IEnumerator AutoFire()
    {
        isFiring = true; // オート射撃中
        while (Input.GetKey(KeyCode.Mouse0) && bulletCount > 0)
        {
            ShootBullet();
            bulletCount--;
            UpdateBulletCountText(); // 弾数を更新
            yield return new WaitForSeconds(autoFireInterval); // オート射撃の間隔
        }
        isFiring = false; // オート射撃終了
    }

    // 弾を発射するメソッド
    void ShootBullet()
    {
        // 弾の生成
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed);

        // 効果音を再生
        PlayShootSE();

        // ステージ外に出たら弾を削除
        StartCoroutine(CheckOutOfBounds(bullet));

        // 弾を一定時間後に破壊
        Destroy(bullet, lifeTime);
    }

    // 効果音を再生するメソッド
    void PlayShootSE()
    {
        if (shootSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSE); // 効果音を一度再生
        }
    }

    // 弾がステージ外に出たかを確認するコルーチン
    IEnumerator CheckOutOfBounds(GameObject bullet)
    {
        while (bullet != null)
        {
            float distanceFromCenter = Vector3.Distance(bullet.transform.position, stageCenter);

            // 最大半径または最小半径を超えたら弾を削除
            if (distanceFromCenter > fieldRadius || distanceFromCenter < minRadius)
            {
                Destroy(bullet);
                yield break;
            }
            yield return new WaitForSeconds(0.1f); // 一定間隔でチェック
        }
    }

    // 弾を追加するメソッド
    public void AddBullets(int amount)
    {
        // 弾数の最大値を超えないようにする
        bulletCount = Mathf.Min(bulletCount + amount, maxBullets);
        UpdateBulletCountText(); // 弾数を更新
    }

    // 弾数を更新するメソッド
    void UpdateBulletCountText()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = "Bullets: " + bulletCount + " / " + maxBullets;
        }
    }

    // 弾数を取得するメソッド
    public int GetBulletCount()
    {
        return bulletCount;
    }
}
