using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    public float invincibilityDuration = 2.0f;  // 無敵時間（秒）
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                // 無敵時間を設定
                isInvincible = true;
                invincibilityTimer = invincibilityDuration;

                // プレイヤーにダメージを与える処理（省略可能）
                // 例: health -= 1;

                Debug.Log("プレイヤーが敵に衝突！無敵状態開始");
            }
            else
            {
                Debug.Log("無敵状態中のため、ダメージなし");
            }
        }
    }
}
