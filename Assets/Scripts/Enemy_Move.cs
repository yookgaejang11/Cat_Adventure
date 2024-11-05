using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    
    private Animator animator;
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spRender;
    public GameManager manager;
    private void Awake()
    {
        manager = FindFirstObjectByType<GameManager>();// GameManager가 장면에 있으면 할당됩니다.
        if (manager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다. 씬에 GameManager가 있는지 확인하세요.");
        }
        spRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //move
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);

        //몬스터가 벽에 떨어지지 않는 방법, 저번에 ray를 쏴서 밑이 허공인지 체크
        //platform check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1,
                                            LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            nextMove *= (-1);
            Turn();
        }
    }

    void Turn()
    {
        //방향전환
        if (nextMove < 0)
            spRender.flipX = false;
        else if (nextMove > 0)
            spRender.flipX = true;
    }

    public void OnDamaged()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("벽 감지");
            nextMove *= (-1);
            Turn();
        }
    }

}