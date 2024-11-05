using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject retryScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Die()
    {
        Time.timeScale = 0;
        retryScreen.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Stage1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            retryScreen.SetActive(true);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
