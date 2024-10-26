using UnityEngine;
using UnityEngine.SceneManagement; // シーン名を取得するために必要

public class ScoreStorage : MonoBehaviour
{
    public static ScoreStorage Instance { get; private set; }

    private int score = 0;
    private int bonusHitCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 現在のシーンが"start"の場合にスコアをリセット
        if (SceneManager.GetActiveScene().name == "start")
        {
            ResetScore();
        }
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public void SubtractScore(int value)
    {
        // スコアが0未満にならないようにする
        score = Mathf.Max(score - value, 0);
    }

    public void AddBonusHitCount()
    {
        bonusHitCount++;
    }

    public void ResetScore()
    {
        score = 0;
        bonusHitCount = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBonusHitCount()
    {
        return bonusHitCount;
    }
}
