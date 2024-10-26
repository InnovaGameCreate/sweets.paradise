using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;      // 移動速度
    [SerializeField] private float fieldRadius = 10f; // フィールドの最大半径
    [SerializeField] private float minRadius = 2f;   // フィールドの最小半径

    void Update()
    {
        // WASDキーによる移動方向の設定
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward; // 前方移動
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward; // 後方移動
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right; // 左移動
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right; // 右移動
        }

        // 移動処理
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        // 現在の位置を取得
        Vector3 currentPosition = transform.position;

        // 移動範囲の制限（最大半径と最小半径）
        float currentDistance = currentPosition.magnitude; // 中心からの距離

        // 最大半径の制限
        if (currentDistance > fieldRadius)
        {
            currentPosition = currentPosition.normalized * fieldRadius;
        }

        // 最小半径の制限
        if (currentDistance < minRadius)
        {
            currentPosition = currentPosition.normalized * minRadius;
        }

        // 制限後の位置を適用
        transform.position = currentPosition;
    }
}
