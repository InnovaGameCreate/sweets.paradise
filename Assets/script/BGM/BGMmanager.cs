using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip;  // 再生するBGMの音声クリップ
    private AudioSource audioSource;

    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = gameObject.AddComponent<AudioSource>();

        // 音声クリップを設定
        audioSource.clip = bgmClip;

        // ループ再生を設定
        audioSource.loop = true;

        // BGMを再生
        audioSource.Play();
    }
}
