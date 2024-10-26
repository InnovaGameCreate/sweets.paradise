using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // 切り替えたいシーンの名前
    public string sceneName;

    // 効果音を再生するためのAudioSource
    public AudioClip se; // 再生したい効果音を格納するAudioClip
    private AudioSource audioSource; // AudioSourceコンポーネント

    // クリックを一度しかできないようにするためのフラグ
    private bool hasClicked = false;

    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // すでにクリックされていたら処理をしない
        if (hasClicked)
        {
            return;
        }

        // マウスの左クリックを検出
        if (Input.GetMouseButtonDown(0))
        {
            // カメラからクリック位置に向かってRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Rayが何かに当たったかを確認
            if (Physics.Raycast(ray, out hit))
            {
                // 当たったオブジェクトのタグが"Clickable"ならシーンを切り替える
                if (hit.collider.CompareTag("Clickable"))
                {
                    // クリックを記録して、再度クリックできないようにする
                    hasClicked = true;

                    // 効果音を再生
                    PlaySE();

                    // シーンを切り替える前に少し待つ
                    Invoke("ChangeScene", se.length);
                }
            }
        }
    }

    // 効果音を再生するメソッド
    void PlaySE()
    {
        audioSource.PlayOneShot(se);
    }

    // シーンを切り替えるメソッド
    void ChangeScene()
    {
        SceneManager.LoadScene("game");
    }
}
