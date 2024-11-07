using UnityEngine;

public class Tuna_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigid.AddForce(new Vector2(-speed, speed), ForceMode2D.Impulse);
    }

}
