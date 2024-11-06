using System.Net;
using UnityEngine;

public class Bullet_Move : MonoBehaviour
{
    public float Bullet_Speed;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigid.AddForce(Vector2.left * Bullet_Speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "End_Point")
        {
            Destroy(gameObject);
        }
    }
}
