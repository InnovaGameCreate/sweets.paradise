using UnityEngine;
using UnityEngine.UI;

public class DigitalTimer : MonoBehaviour
{
    public Sprite[] numberSprites;  // 0����9�̃X�v���C�g���i�[����z��
    public Image minutesTensPlaceImage;  // ����10�̈ʂ̐����̕\��
    public Image minutesOnesPlaceImage;  // ����1�̈ʂ̐����̕\��
    public Image secondsTensPlaceImage;  // �b��10�̈ʂ̐����̕\��
    public Image secondsOnesPlaceImage;  // �b��1�̈ʂ̐����̕\��
    private float timeRemaining = 180f;  // 3���i180�b�j�̃^�C�}�[

    void Start()
    {
        // �^�C�}�[���J�n�����Ƃ��ɏ�����Ԃ̕\����ݒ�
        UpdateTimerDisplay(0, 0, 0, 0);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            // �^�C�}�[�����炷
            timeRemaining -= Time.deltaTime;

            // �c�莞�Ԃ𐮐��ɕϊ�
            int totalSeconds = Mathf.CeilToInt(timeRemaining);

            // ���ƕb�ɕ���
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // 10�̈ʂ�1�̈ʂɕ���
            int minutesTensPlace = minutes / 10;
            int minutesOnesPlace = minutes % 10;
            int secondsTensPlace = seconds / 10;
            int secondsOnesPlace = seconds % 10;

            // �f�o�b�O���O��ǉ����Čv�Z�l���m�F
            Debug.Log($"Total Seconds: {totalSeconds}, Minutes: {minutes}, Seconds: {seconds}");
            Debug.Log($"Minutes Tens: {minutesTensPlace}, Minutes Ones: {minutesOnesPlace}");
            Debug.Log($"Seconds Tens: {secondsTensPlace}, Seconds Ones: {secondsOnesPlace}");

            // �摜�̕\�����X�V
            UpdateTimerDisplay(minutesTensPlace, minutesOnesPlace, secondsTensPlace, secondsOnesPlace);
        }
        else
        {
            // �^�C�}�[��0�ɂȂ����Ƃ��̏����i�I�v�V�����j
            timeRemaining = 0;
        }
    }

    void UpdateTimerDisplay(int minutesTensPlace, int minutesOnesPlace, int secondsTensPlace, int secondsOnesPlace)
    {
        // �C���f�b�N�X�͈̔̓`�F�b�N��null�`�F�b�N
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