using UnityEngine;

public class Sand_Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            Debug.Log("�𷡼�ȯ��");
            gameObject.SetActive(true);
        }
    }
}
