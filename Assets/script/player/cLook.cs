using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // �}�E�X���x

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �J�[�\���𒆉��ɌŒ肵�Ĕ�\��
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Y���i���E�j�̉�]��K�p
        transform.Rotate(Vector3.up * mouseX);
    }
}
