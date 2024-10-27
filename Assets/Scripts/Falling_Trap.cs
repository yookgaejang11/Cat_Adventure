using UnityEngine;

public class Falling_Trap : MonoBehaviour
{
    bool isFalling = false;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(isFalling)
        {
            transform.position = new Vector2(0,transform.position.y - 0.015f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("����");
            isFalling = true;
        }
    }
}
