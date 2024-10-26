using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;      // �ړ����x
    [SerializeField] private float fieldRadius = 10f; // �t�B�[���h�̍ő唼�a
    [SerializeField] private float minRadius = 2f;   // �t�B�[���h�̍ŏ����a

    void Update()
    {
        // WASD�L�[�ɂ��ړ������̐ݒ�
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward; // �O���ړ�
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward; // ����ړ�
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= transform.right; // ���ړ�
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right; // �E�ړ�
        }

        // �ړ�����
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);

        // ���݂̈ʒu���擾
        Vector3 currentPosition = transform.position;

        // �ړ��͈͂̐����i�ő唼�a�ƍŏ����a�j
        float currentDistance = currentPosition.magnitude; // ���S����̋���

        // �ő唼�a�̐���
        if (currentDistance > fieldRadius)
        {
            currentPosition = currentPosition.normalized * fieldRadius;
        }

        // �ŏ����a�̐���
        if (currentDistance < minRadius)
        {
            currentPosition = currentPosition.normalized * minRadius;
        }

        // ������̈ʒu��K�p
        transform.position = currentPosition;
    }
}
