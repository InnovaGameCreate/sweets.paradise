using UnityEngine;
using UnityEngine.UI;

public class ResultScoreUI : MonoBehaviour
{
    public Text resultScoreText; // リザルトシーンのスコア表示用Text
    public Text resultBonusHitText; // リザルトシーンのボーナスヒット表示用Text

    void Start()
    {
        // ScoreStorageのインスタンスが存在するかチェック
        if (ScoreStorage.Instance != null)
        {
            // ScoreStorageからスコアとボーナスヒットカウントを取得してテキストに反映
            resultScoreText.text = ScoreStorage.Instance.GetScore().ToString();
            resultBonusHitText.text = ScoreStorage.Instance.GetBonusHitCount().ToString();
        }
        else
        {
            Debug.LogError("ScoreStorage is not found in the Result Scene.");
        }
    }
}
