using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    public float invincibilityDuration = 2.0f;  // ���G���ԁi�b�j
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                // ���G���Ԃ�ݒ�
                isInvincible = true;
                invincibilityTimer = invincibilityDuration;

                // �v���C���[�Ƀ_���[�W��^���鏈���i�ȗ��\�j
                // ��: health -= 1;

                Debug.Log("�v���C���[���G�ɏՓˁI���G��ԊJ�n");
            }
            else
            {
                Debug.Log("���G��Ԓ��̂��߁A�_���[�W�Ȃ�");
            }
        }
    }
}
