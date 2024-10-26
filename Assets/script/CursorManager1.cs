using UnityEngine;

public class CursorsorManager1: MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // シーン2ではカーソルをロックしない
        Cursor.visible = true; // カーソルを表示する
    }
}
