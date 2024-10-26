using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // マウス感度

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルを中央に固定して非表示
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Y軸（左右）の回転を適用
        transform.Rotate(Vector3.up * mouseX);
    }
}
