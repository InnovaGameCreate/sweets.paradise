using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;  // スコアを表示するTextコンポーネント
    private ScoreManager1 scoreManager;  // スコアマネージャーの参照

    void Start()
    {
        // ScoreManagerのインスタンスを取得
        scoreManager = FindObjectOfType<ScoreManager1>();
    }

    void Update()
    {
        // scoreManagerとscoreTextがnullでない場合のみスコアを更新
        if (scoreManager != null && scoreText != null)
        {
            scoreText.text = "スコア：" + scoreManager.GetScore().ToString();
        }
    }
}
