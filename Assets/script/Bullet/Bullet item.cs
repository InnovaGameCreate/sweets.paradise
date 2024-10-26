using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            if (bulletMove != null)
            {
                bulletMove.AddBullets(1); 
                Destroy(gameObject); // ���̃I�u�W�F�N�g���폜
            }
            else
            {
                Debug.LogWarning("BulletMove �R���|�[�l���g���v���C���[�Ɍ�����܂���B");
            }
        }
    }
}
