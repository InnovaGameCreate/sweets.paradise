using UnityEngine;
using UnityEngine.SceneManagement;

public class resulttriger : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {        
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;     
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Clickable"))
                {
                    SceneManager.LoadScene("start");
                }
            }
        }
    }
}
