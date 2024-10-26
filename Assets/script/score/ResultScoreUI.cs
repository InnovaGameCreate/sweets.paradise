using UnityEngine;
using UnityEngine.UI;

public class ResultScoreUI : MonoBehaviour
{
    public Text resultScoreText; // ���U���g�V�[���̃X�R�A�\���pText
    public Text resultBonusHitText; // ���U���g�V�[���̃{�[�i�X�q�b�g�\���pText

    void Start()
    {
        // ScoreStorage�̃C���X�^���X�����݂��邩�`�F�b�N
        if (ScoreStorage.Instance != null)
        {
            // ScoreStorage����X�R�A�ƃ{�[�i�X�q�b�g�J�E���g���擾���ăe�L�X�g�ɔ��f
            resultScoreText.text = ScoreStorage.Instance.GetScore().ToString();
            resultBonusHitText.text = ScoreStorage.Instance.GetBonusHitCount().ToString();
        }
        else
        {
            Debug.LogError("ScoreStorage is not found in the Result Scene.");
        }
    }
}
