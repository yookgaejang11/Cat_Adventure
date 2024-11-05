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
        manager = FindFirstObjectByType<GameManager>();// GameManager�� ��鿡 ������ �Ҵ�˴ϴ�.
        if (manager == null)
        {
            Debug.LogError("GameManager�� ã�� �� �����ϴ�. ���� GameManager�� �ִ��� Ȯ���ϼ���.");
        }
        spRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //move
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);

        //���Ͱ� ���� �������� �ʴ� ���, ������ ray�� ���� ���� ������� üũ
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
        //������ȯ
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
            Debug.Log("�� ����");
            nextMove *= (-1);
            Turn();
        }
    }

}