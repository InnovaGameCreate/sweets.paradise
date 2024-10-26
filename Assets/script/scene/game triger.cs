using UnityEngine;
using UnityEngine.SceneManagement;

public class gametriger : MonoBehaviour
{
    public float timeToWait = 180f; 

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToWait)
        {
            SceneManager.LoadScene("result");
        }
    }
}
