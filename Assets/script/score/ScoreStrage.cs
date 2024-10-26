using UnityEngine;
using UnityEngine.SceneManagement; // �V�[�������擾���邽�߂ɕK�v

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
        // ���݂̃V�[����"start"�̏ꍇ�ɃX�R�A�����Z�b�g
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
        // �X�R�A��0�����ɂȂ�Ȃ��悤�ɂ���
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
