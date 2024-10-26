using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eat : MonoBehaviour
{
    // 効果音のAudioClipを設定
    [SerializeField] private AudioClip eatSE; // エネミーに触れた時の効果音
    private AudioSource audioSource; // AudioSourceコンポーネント

    // Start is called before the first frame update
    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = gameObject.AddComponent<AudioSource>();

        // タグ "Enemy" のオブジェクトを取得
        GameObject Enemy = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // トリガーにエネミーが触れた際の処理
    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.CompareTag("Enemy"))
        {
            // 効果音を再生
            PlayEatSE();

            // 触れた敵を削除
            Destroy(Enemy.gameObject);
        }
    }

    // 効果音を再生するメソッド
    void PlayEatSE()
    {
        if (eatSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(eatSE); // 効果音を一度再生
        }
    }
}
