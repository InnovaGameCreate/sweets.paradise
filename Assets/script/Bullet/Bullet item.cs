using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            if (bulletMove != null)
            {
                bulletMove.AddBullets(1); 
                Destroy(gameObject); // このオブジェクトを削除
            }
            else
            {
                Debug.LogWarning("BulletMove コンポーネントがプレイヤーに見つかりません。");
            }
        }
    }
}
