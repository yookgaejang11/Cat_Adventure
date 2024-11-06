using UnityEngine;

public class Front_Enemy : MonoBehaviour
{
    public float Speed;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void OnDamaged()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(-Speed, 0);
    }
}
