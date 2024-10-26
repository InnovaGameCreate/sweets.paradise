using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eat : MonoBehaviour
{
    // ���ʉ���AudioClip��ݒ�
    [SerializeField] private AudioClip eatSE; // �G�l�~�[�ɐG�ꂽ���̌��ʉ�
    private AudioSource audioSource; // AudioSource�R���|�[�l���g

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource�R���|�[�l���g���擾
        audioSource = gameObject.AddComponent<AudioSource>();

        // �^�O "Enemy" �̃I�u�W�F�N�g���擾
        GameObject Enemy = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �g���K�[�ɃG�l�~�[���G�ꂽ�ۂ̏���
    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.CompareTag("Enemy"))
        {
            // ���ʉ����Đ�
            PlayEatSE();

            // �G�ꂽ�G���폜
            Destroy(Enemy.gameObject);
        }
    }

    // ���ʉ����Đ����郁�\�b�h
    void PlayEatSE()
    {
        if (eatSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(eatSE); // ���ʉ�����x�Đ�
        }
    }
}
