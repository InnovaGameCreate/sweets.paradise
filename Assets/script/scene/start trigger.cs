using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    // �؂�ւ������V�[���̖��O
    public string sceneName;

    // ���ʉ����Đ����邽�߂�AudioSource
    public AudioClip se; // �Đ����������ʉ����i�[����AudioClip
    private AudioSource audioSource; // AudioSource�R���|�[�l���g

    // �N���b�N����x�����ł��Ȃ��悤�ɂ��邽�߂̃t���O
    private bool hasClicked = false;

    void Start()
    {
        // AudioSource�R���|�[�l���g���擾
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // ���łɃN���b�N����Ă����珈�������Ȃ�
        if (hasClicked)
        {
            return;
        }

        // �}�E�X�̍��N���b�N�����o
        if (Input.GetMouseButtonDown(0))
        {
            // �J��������N���b�N�ʒu�Ɍ�������Ray���΂�
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ray�������ɓ������������m�F
            if (Physics.Raycast(ray, out hit))
            {
                // ���������I�u�W�F�N�g�̃^�O��"Clickable"�Ȃ�V�[����؂�ւ���
                if (hit.collider.CompareTag("Clickable"))
                {
                    // �N���b�N���L�^���āA�ēx�N���b�N�ł��Ȃ��悤�ɂ���
                    hasClicked = true;

                    // ���ʉ����Đ�
                    PlaySE();

                    // �V�[����؂�ւ���O�ɏ����҂�
                    Invoke("ChangeScene", se.length);
                }
            }
        }
    }

    // ���ʉ����Đ����郁�\�b�h
    void PlaySE()
    {
        audioSource.PlayOneShot(se);
    }

    // �V�[����؂�ւ��郁�\�b�h
    void ChangeScene()
    {
        SceneManager.LoadScene("game");
    }
}
