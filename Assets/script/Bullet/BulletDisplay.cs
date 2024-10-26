using UnityEngine;
using UnityEngine.UI;

public class BulletDisplay : MonoBehaviour
{
    [SerializeField] private BulletMove bulletMoveScript; // BulletMove スクリプトへの参照
    [SerializeField] private Image bulletImagePrefab; // 弾の数を表示するための画像プレハブ
    [SerializeField] private Transform displayParent; // 画像を配置する親オブジェクト

    private void Start()
    {
        UpdateBulletDisplay();
    }

    private void Update()
    {
        if (bulletMoveScript != null)
        {
            // BulletMove スクリプトから弾の数を取得して表示を更新
            UpdateBulletDisplay();
        }
    }

    private void UpdateBulletDisplay()
    {
        // 既存の画像を削除
        foreach (Transform child in displayParent)
        {
            Destroy(child.gameObject);
        }

        // 弾の数に応じて画像を配置
        int bulletCount = bulletMoveScript.GetBulletCount();
        float spacing = 1 / 8f; // スペーシングの変数を追加
        for (int i = 0; i < bulletCount; i++)
        {
            float s = i * spacing; // s を計算
            Image bulletImage = Instantiate(bulletImagePrefab, displayParent);
            bulletImage.transform.localPosition = new Vector3(-s, 0, 0); // x 軸方向にずらして配置
        }
    }
}
