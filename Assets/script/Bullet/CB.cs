using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB : MonoBehaviour
{
    public GameObject objectToSpawn; // 生成するオブジェクト
    public float spawnInterval = 2f; // 生成間隔
    [SerializeField] private float maxRadius = 115f; // 最大半径
    [SerializeField] private float minRadius = 20f;  // 最小半径

    private float timeSinceLastSpawn;
    private GameObject makeAmmo;
    [SerializeField][Tooltip("生成する弾の数")] private int sum; // 弾の総数
    private int cnt; // 生成された弾の数

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0f; // スポーンタイマーの初期化
        cnt = 0; // 生成された弾の数を初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt < sum)
        {
            timeSinceLastSpawn += Time.deltaTime; // 経過時間をカウント

            // 生成間隔を超えたら弾を生成
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnObject(); // 弾を生成
                cnt++; // 弾の数をカウント
                timeSinceLastSpawn = 0f; // タイマーをリセット
            }
        }
    }

    // 弾を生成する関数
    void SpawnObject()
    {
        // ステージの最大・最小範囲内でランダムな距離を取得
        float distance = Random.Range(minRadius, maxRadius); // 最小と最大の間の距離

        // ランダムな角度を取得
        float angle = Random.Range(0f, 2 * Mathf.PI); // 0から2πまでのランダムな角度

        // 極座標からXY座標へ変換して位置を計算
        Vector3 randomPosition = new Vector3(
            distance * Mathf.Cos(angle),
            0f, // 高さは0に固定（平面上での生成）
            distance * Mathf.Sin(angle)
        );

        Vector3 spawnPosition = transform.position + randomPosition; // スポーン位置を計算

        // プレファブのローテーションを適用
        Quaternion spawnRotation = Quaternion.Euler(-90, Random.Range(0, 360f), 0); // 回転を指定（例: Y軸回転）

        // 弾を生成
        makeAmmo = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

        // Ammoコンポーネントを設定
        makeAmmo.GetComponent<Ammo>().SetCB(this);
    }

    // 弾が破壊された時に呼ばれる関数
    public void DecreaseCount()
    {
        cnt--; // 弾のカウントを減らす
    }
}
