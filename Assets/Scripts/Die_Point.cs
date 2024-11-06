using UnityEngine;

public class Die_Point : MonoBehaviour
{
    GameManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<GameManager>();// GameManager가 장면에 있으면 할당됩니다.
        if (manager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다. 씬에 GameManager가 있는지 확인하세요.");
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
