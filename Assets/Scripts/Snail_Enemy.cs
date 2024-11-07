using UnityEngine;

public class Snail_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject explosionPrefab; // ������ ������
    private Animator animator;
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spRender;
    public GameManager manager;
    public float jumpForce = 10f; // ���� ��
    public float rayLength = 1.0f; // Raycast ����
    public LayerMask playerLayer; // �÷��̾� ���̾� ����ũ
    public float rayOffsetX = 0.1f; // Raycast�� X ������
    AudioSource SnailDie;
    private void Awake()
    {
        
        spRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        Debug.Log(animator != null);
        manager = FindFirstObjectByType<GameManager>();// GameManager�� ��鿡 ������ �Ҵ�˴ϴ�.
        if (manager == null)
        {
            Debug.LogError("GameManager�� ã�� �� �����ϴ�. ���� GameManager�� �ִ��� Ȯ���ϼ���.");
        }


    }

    public void OnDamaged()
    {
        
        if (animator.GetBool("isStep"))
        {
            Vector3 currentPosition = transform.position = new Vector3(transform.position.x, transform.position.y + 1.75f, 0);
            Quaternion currentRotation = transform.rotation;

            // ���� ������Ʈ ����
            Destroy(gameObject);
            spRender.color = Color.red;
            // ���ο� ������Ʈ ����
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, currentPosition, currentRotation);
            }
        }
        SnailDie.Play();
        animator.SetBool("isStep", true);
        nextMove = 0;
    }
    private void FixedUpdate()
    {
        //move
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocity.y);

        //���Ͱ� ���� �������� �ʴ� ���, ������ ray�� ���� ���� ������� üũ
        //platform check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1,LayerMask.GetMask("Platform"));

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


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("������");
            nextMove *= (-1);
            Turn();
        }
    }


}
