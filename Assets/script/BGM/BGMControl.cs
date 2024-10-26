using UnityEngine;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioClip initialBGMClip; // �ŏ���BGM�N���b�v
    [SerializeField] private AudioClip changedBGMClip; // �؂�ւ����BGM�N���b�v
    [SerializeField] private AudioClip transitionClip; // 3�b�Ԃ̒Z�����y
    [SerializeField] private float switchTime = 150f;  // BGM��؂�ւ��鎞�ԁi�b�A�����ł�150�b = 2��30�b�j

    private AudioSource audioSource;
    private float elapsedTime = 0f; // �o�ߎ���
    private bool hasTransitioned = false; // 3�b�Ԃ̒Z�����y���Đ����ꂽ���ǂ���

    void Start()
    {
        // AudioSource �R���|�[�l���g��ǉ����A�ݒ肵�܂�
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = initialBGMClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // �w��̎��ԂɂȂ�����A�܂�3�b�Ԃ̒Z�����y���Đ�
        if (elapsedTime >= switchTime && !hasTransitioned)
        {
            audioSource.Stop();
            audioSource.clip = transitionClip;
            audioSource.loop = false; // 3�b�̉��y�̓��[�v�����Ȃ�
            audioSource.Play();
            hasTransitioned = true;
        }

        // 3�b�̉��y���I�������A�V����BGM���Đ�
        if (hasTransitioned && !audioSource.isPlaying)
        {
            audioSource.clip = changedBGMClip;
            audioSource.loop = true; // �V����BGM�̓��[�v������
            audioSource.Play();
        }
    }
}
