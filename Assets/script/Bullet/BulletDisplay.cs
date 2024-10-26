using UnityEngine;
using UnityEngine.UI;

public class BulletDisplay : MonoBehaviour
{
    [SerializeField] private BulletMove bulletMoveScript; // BulletMove �X�N���v�g�ւ̎Q��
    [SerializeField] private Image bulletImagePrefab; // �e�̐���\�����邽�߂̉摜�v���n�u
    [SerializeField] private Transform displayParent; // �摜��z�u����e�I�u�W�F�N�g

    private void Start()
    {
        UpdateBulletDisplay();
    }

    private void Update()
    {
        if (bulletMoveScript != null)
        {
            // BulletMove �X�N���v�g����e�̐����擾���ĕ\�����X�V
            UpdateBulletDisplay();
        }
    }

    private void UpdateBulletDisplay()
    {
        // �����̉摜���폜
        foreach (Transform child in displayParent)
        {
            Destroy(child.gameObject);
        }

        // �e�̐��ɉ����ĉ摜��z�u
        int bulletCount = bulletMoveScript.GetBulletCount();
        float spacing = 1 / 8f; // �X�y�[�V���O�̕ϐ���ǉ�
        for (int i = 0; i < bulletCount; i++)
        {
            float s = i * spacing; // s ���v�Z
            Image bulletImage = Instantiate(bulletImagePrefab, displayParent);
            bulletImage.transform.localPosition = new Vector3(-s, 0, 0); // x �������ɂ��炵�Ĕz�u
        }
    }
}
