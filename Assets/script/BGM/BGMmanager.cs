using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip;  // �Đ�����BGM�̉����N���b�v
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource�R���|�[�l���g���擾
        audioSource = gameObject.AddComponent<AudioSource>();

        // �����N���b�v��ݒ�
        audioSource.clip = bgmClip;

        // ���[�v�Đ���ݒ�
        audioSource.loop = true;

        // BGM���Đ�
        audioSource.Play();
    }
}
