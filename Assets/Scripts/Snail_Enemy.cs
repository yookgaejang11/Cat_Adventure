using UnityEngine;

public class Snail_Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject explosionPrefab; // 폭발할 프리팹
    private Animator animator;
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spRender;
    public GameManager manager;
    public float jumpForce = 10f; // 점프 힘
    public float rayLength = 1.0f; // Raycast 길이
    public LayerMask playerLayer; // 플레이어 레이어 마스크
    public float rayOffsetX = 0.1f; // Raycast의 X 오프셋
    AudioSource SnailDie;
    private void Awake()
    {
        
        spRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        Debug.Log(animator != null);
        manager = FindFirstObjectByType<GameManager>();// GameManager가 장면에 있으면 할당됩니다.
        if (manager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다. 씬에 GameManager가 있는지 확인하세요.");
        }


    }

    public void OnDamaged()
    {
        
        if (animator.GetBool("isStep"))
        {
            Vector3 currentPosition = transform.position = new Vector3(transform.position.x, transform.position.y + 1.75f, 0);
            Quaternion currentRotation = transform.rotation;

            // 현재 오브젝트 삭제
            Destroy(gameObject);
            spRender.color = Color.red;
            // 새로운 오브젝트 생성
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

        //몬스터가 벽에 떨어지지 않는 방법, 저번에 ray를 쏴서 밑이 허공인지 체크
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
        //방향전환
        if (nextMove < 0)
            spRender.flipX = false;
        else if (nextMove > 0)
            spRender.flipX = true;
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("벽감지");
            nextMove *= (-1);
            Turn();
        }
    }


}
