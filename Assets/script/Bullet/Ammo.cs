using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private CB cB;

    // CBクラスの参照を設定
    public void SetCB(CB setCB)
    {
        cB = setCB;
    }

    // 弾丸が削除される時にカウントを減らす
    private void OnDestroy()
    {
        if (cB != null)
        {
            cB.DecreaseCount(); // メソッド名を修正
        }
    }

    // 衝突検知
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waffle")) // 特定のタグに当たった場合
        {
            Destroy(this.gameObject); // 弾丸を削除
        }
    }
}

