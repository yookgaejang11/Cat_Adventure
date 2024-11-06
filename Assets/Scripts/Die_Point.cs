using UnityEngine;

public class Die_Point : MonoBehaviour
{
    GameManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<GameManager>();// GameManager�� ��鿡 ������ �Ҵ�˴ϴ�.
        if (manager == null)
        {
            Debug.LogError("GameManager�� ã�� �� �����ϴ�. ���� GameManager�� �ִ��� Ȯ���ϼ���.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            manager.Die();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }




}
