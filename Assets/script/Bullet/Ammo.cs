using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private CB cB;

    // CB�N���X�̎Q�Ƃ�ݒ�
    public void SetCB(CB setCB)
    {
        cB = setCB;
    }

    // �e�ۂ��폜����鎞�ɃJ�E���g�����炷
    private void OnDestroy()
    {
        if (cB != null)
        {
            cB.DecreaseCount(); // ���\�b�h�����C��
        }
    }

    // �Փˌ��m
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waffle")) // ����̃^�O�ɓ��������ꍇ
        {
            Destroy(this.gameObject); // �e�ۂ��폜
        }
    }
}

