using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioClip initialBGMClip; // 最初のBGMクリップ
    [SerializeField] private AudioClip changedBGMClip; // 切り替え後のBGMクリップ
    [SerializeField] private AudioClip transitionClip; // 3秒間の短い音楽
    [SerializeField] private float switchTime = 150f;  // BGMを切り替える時間（秒、ここでは150秒 = 2分30秒）

    private AudioSource audioSource;
    private float elapsedTime = 0f; // 経過時間
    private bool hasTransitioned = false; // 3秒間の短い音楽が再生されたかどうか

    void Start()
    {
        // AudioSource コンポーネントを追加し、設定します
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = initialBGMClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // 指定の時間になったら、まず3秒間の短い音楽を再生
        if (elapsedTime >= switchTime && !hasTransitioned)
        {
            audioSource.Stop();
            audioSource.clip = transitionClip;
            audioSource.loop = false; // 3秒の音楽はループさせない
            audioSource.Play();
            hasTransitioned = true;
        }

        // 3秒の音楽が終わったら、新しいBGMを再生
        if (hasTransitioned && !audioSource.isPlaying)
        {
            audioSource.clip = changedBGMClip;
            audioSource.loop = true; // 新しいBGMはループさせる
            audioSource.Play();
        }
    }
}
