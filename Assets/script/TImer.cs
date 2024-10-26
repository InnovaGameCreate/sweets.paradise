using UnityEngine;
using UnityEngine.UI;

public class DigitalTimer : MonoBehaviour
{
    public Sprite[] numberSprites;  // 0から9のスプライトを格納する配列
    public Image minutesTensPlaceImage;  // 分の10の位の数字の表示
    public Image minutesOnesPlaceImage;  // 分の1の位の数字の表示
    public Image secondsTensPlaceImage;  // 秒の10の位の数字の表示
    public Image secondsOnesPlaceImage;  // 秒の1の位の数字の表示
    private float timeRemaining = 180f;  // 3分（180秒）のタイマー

    void Start()
    {
        // タイマーが開始したときに初期状態の表示を設定
        UpdateTimerDisplay(0, 0, 0, 0);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            // タイマーを減らす
            timeRemaining -= Time.deltaTime;

            // 残り時間を整数に変換
            int totalSeconds = Mathf.CeilToInt(timeRemaining);

            // 分と秒に分割
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // 10の位と1の位に分割
            int minutesTensPlace = minutes / 10;
            int minutesOnesPlace = minutes % 10;
            int secondsTensPlace = seconds / 10;
            int secondsOnesPlace = seconds % 10;

            // デバッグログを追加して計算値を確認
            Debug.Log($"Total Seconds: {totalSeconds}, Minutes: {minutes}, Seconds: {seconds}");
            Debug.Log($"Minutes Tens: {minutesTensPlace}, Minutes Ones: {minutesOnesPlace}");
            Debug.Log($"Seconds Tens: {secondsTensPlace}, Seconds Ones: {secondsOnesPlace}");

            // 画像の表示を更新
            UpdateTimerDisplay(minutesTensPlace, minutesOnesPlace, secondsTensPlace, secondsOnesPlace);
        }
        else
        {
            // タイマーが0になったときの処理（オプション）
            timeRemaining = 0;
        }
    }

    void UpdateTimerDisplay(int minutesTensPlace, int minutesOnesPlace, int secondsTensPlace, int secondsOnesPlace)
    {
        // インデックスの範囲チェックとnullチェック
        if (minutesTensPlace >= 0 && minutesTensPlace <= 9)
        {
            if (numberSprites.Length > minutesTensPlace && numberSprites[minutesTensPlace] != null)
            {
                minutesTensPlaceImage.sprite = numberSprites[minutesTensPlace];
            }
            else
            {
                Debug.LogError($"Sprite for minutes tens place ({minutesTensPlace}) is null or out of range.");
            }
        }
        else
        {
            Debug.LogError($"Invalid index for minutes tens place: {minutesTensPlace}");
        }

        if (minutesOnesPlace >= 0 && minutesOnesPlace <= 9)
        {
            if (numberSprites.Length > minutesOnesPlace && numberSprites[minutesOnesPlace] != null)
            {
                minutesOnesPlaceImage.sprite = numberSprites[minutesOnesPlace];
            }
            else
            {
                Debug.LogError($"Sprite for minutes ones place ({minutesOnesPlace}) is null or out of range.");
            }
        }
        else
        {
            Debug.LogError($"Invalid index for minutes ones place: {minutesOnesPlace}");
        }

        if (secondsTensPlace >= 0 && secondsTensPlace <= 9)
        {
            if (numberSprites.Length > secondsTensPlace && numberSprites[secondsTensPlace] != null)
            {
                secondsTensPlaceImage.sprite = numberSprites[secondsTensPlace];
            }
            else
            {
                Debug.LogError($"Sprite for seconds tens place ({secondsTensPlace}) is null or out of range.");
            }
        }
        else
        {
            Debug.LogError($"Invalid index for seconds tens place: {secondsTensPlace}");
        }

        if (secondsOnesPlace >= 0 && secondsOnesPlace <= 9)
        {
            if (numberSprites.Length > secondsOnesPlace && numberSprites[secondsOnesPlace] != null)
            {
                secondsOnesPlaceImage.sprite = numberSprites[secondsOnesPlace];
            }
            else
            {
                Debug.LogError($"Sprite for seconds ones place ({secondsOnesPlace}) is null or out of range.");
            }
        }
        else
        {
            Debug.LogError($"Invalid index for seconds ones place: {secondsOnesPlace}");
        }
    }

}