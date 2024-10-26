using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;  // �X�R�A��\������Text�R���|�[�l���g
    private ScoreManager1 scoreManager;  // �X�R�A�}�l�[�W���[�̎Q��

    void Start()
    {
        // ScoreManager�̃C���X�^���X���擾
        scoreManager = FindObjectOfType<ScoreManager1>();
    }

    void Update()
    {
        // scoreManager��scoreText��null�łȂ��ꍇ�̂݃X�R�A���X�V
        if (scoreManager != null && scoreText != null)
        {
            scoreText.text = "�X�R�A�F" + scoreManager.GetScore().ToString();
        }
    }
}
