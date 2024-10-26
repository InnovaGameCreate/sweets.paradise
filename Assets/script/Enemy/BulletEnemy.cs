using System.Collections;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float colliderDisableTime = 0.2f; // コリジョン無効にする時間
    [SerializeField] private float lifeTime = 5f; // 弾の寿命
    [SerializeField] private Vector3 stageCenter = Vector3.zero; // ステージの中心点 (0,0,0 に設定)
    [SerializeField] private float stageRadius = 50f; // ステージの半径

    private Collider bulletCollider;

    void Start()
    {
        bulletCollider = GetComponent<Collider>();
        StartCoroutine(DisableColliderTemporarily());

        Destroy(gameObject, lifeTime); // 一定時間後に弾を削除
    }

    void Update()
    {
        // ステージの範囲を超えたら削除
        float distanceFromCenter = Vector3.Distance(transform.position, stageCenter);
        if (distanceFromCenter > stageRadius)
        {
            Destroy(gameObject); // ステージ範囲外に出たら弾を削除
        }
    }

    // コリジョンを一時的に無効にするコルーチン
    IEnumerator DisableColliderTemporarily()
    {
        if (bulletCollider != null)
        {
            bulletCollider.enabled = false; // コリジョン無効化
            yield return new WaitForSeconds(colliderDisableTime); // 指定時間待つ
            bulletCollider.enabled = true; // コリジョン再有効化
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Enemy" タグのオブジェクトに衝突した場合、自分を削除
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject); // 弾を削除
        }
        // "BulletEnemy" タグのオブジェクトに衝突した場合、両方を削除
        else if (other.gameObject.CompareTag("BulletEnemy"))
        {
            Destroy(other.gameObject); // 衝突相手の弾を削除
            Destroy(this.gameObject);  // 自分の弾も削除
        }
    }
}
