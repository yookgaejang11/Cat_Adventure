using UnityEngine;

public class Hide_Block : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject HideBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("¥Í¿Ω" + HideBlock);
            HideBlock.SetActive(true);
        }
           
    }
}
