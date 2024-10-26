using System.Collections;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float colliderDisableTime = 0.2f; // �R���W���������ɂ��鎞��
    [SerializeField] private float lifeTime = 5f; // �e�̎���
    [SerializeField] private Vector3 stageCenter = Vector3.zero; // �X�e�[�W�̒��S�_ (0,0,0 �ɐݒ�)
    [SerializeField] private float stageRadius = 50f; // �X�e�[�W�̔��a

    private Collider bulletCollider;

    void Start()
    {
        bulletCollider = GetComponent<Collider>();
        StartCoroutine(DisableColliderTemporarily());

        Destroy(gameObject, lifeTime); // ��莞�Ԍ�ɒe���폜
    }

    void Update()
    {
        // �X�e�[�W�͈̔͂𒴂�����폜
        float distanceFromCenter = Vector3.Distance(transform.position, stageCenter);
        if (distanceFromCenter > stageRadius)
        {
            Destroy(gameObject); // �X�e�[�W�͈͊O�ɏo����e���폜
        }
    }

    // �R���W�������ꎞ�I�ɖ����ɂ���R���[�`��
    IEnumerator DisableColliderTemporarily()
    {
        if (bulletCollider != null)
        {
            bulletCollider.enabled = false; // �R���W����������
            yield return new WaitForSeconds(colliderDisableTime); // �w�莞�ԑ҂�
            bulletCollider.enabled = true; // �R���W�����ėL����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // "Enemy" �^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ�A�������폜
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject); // �e���폜
        }
        // "BulletEnemy" �^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ�A�������폜
        else if (other.gameObject.CompareTag("BulletEnemy"))
        {
            Destroy(other.gameObject); // �Փˑ���̒e���폜
            Destroy(this.gameObject);  // �����̒e���폜
        }
    }
}
