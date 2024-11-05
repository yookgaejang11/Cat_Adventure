using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEditor;
public class PlayerMove : MonoBehaviour
{
    public GameObject Hideblock;
    public float EnemyRayLength;
    public GameManager manager;
    public GameObject blindBlock;
    public float rayLength;
    bool isSmear = false;
    public GameObject Sand_Trap;
    public float currentSpeed;
    SpriteRenderer spriteRenderer;
    public float MaxSpeed;
    public float Jump_power;
    Rigidbody2D rigid;
    Animator animator;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        manager = FindFirstObjectByType<GameManager>();// GameManager가 장면에 있으면 할당됩니다.
        if (manager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다. 씬에 GameManager가 있는지 확인하세요.");
        }
    }
    private void Update()
    {
       
        if (isSmear)
        {
            Jump_power = 0;
            MaxSpeed = 0;
            currentSpeed = 0;
            rigid.gravityScale = 0;
            rigid.AddForce(new Vector2(0,-0.01f), ForceMode2D.Impulse);
        }
        currentSpeed = rigid.linearVelocityX; //현재 속도 계산
        //Player의 속도가 급격히 멈춤(normalized)
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocityX * 0.2f, rigid.linearVelocityY);
            CancelInvoke("StopPlayer");
            Invoke("StopPlayer", 0.3f);
        }
        //Player Jump
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * Jump_power, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }
       

        //Player flip
        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; //입력이 -1면 true를 반환하고, 그렇지 않으면 false를 반환
        }

        //Player Move animation
        if (Mathf.Abs(rigid.linearVelocityX) < 0.3f)
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }


    }
    void StopPlayer()
        {
            // 일정 시간 후에 플레이어의 속도를 0으로 설정
            if(Mathf.Abs(currentSpeed) > 0)
            currentSpeed = 0;
            rigid.linearVelocityX = currentSpeed;// x축 속도를 0으로 설정
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player move
        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        rigid.AddForce(vec, ForceMode2D.Impulse);

        //player Max Speed
        if (rigid.linearVelocityX > MaxSpeed)
        {
            rigid.linearVelocity = new Vector2(MaxSpeed, rigid.linearVelocityY);//velocity 는 현재 속력
        }
        if (rigid.linearVelocityX < MaxSpeed * (-1))
        {
            rigid.linearVelocity = new Vector2(MaxSpeed * (-1), rigid.linearVelocityY);//velocity 는 현재 속력
        }

        //Landing platform
        if (rigid.linearVelocity.y <= 0)
        {
            Vector2 rayOrigin = new Vector2(rigid.position.x, rigid.position.y - 0.1f); // Ray 시작 위치를 조금 아래로 조정
            rayLength = 1.0f;

            Debug.DrawRay(rigid.position, Vector3.down*rayLength, new Color(1, 0, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, rayLength, LayerMask.GetMask("Platform","Wall"));


            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                    animator.SetBool("isJump", false);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sand_Trap")
        {
            isSmear = true;
        }
        if ((collision.gameObject.tag == "BushTrigger"))
        {
            Debug.Log("모래소환술");
            blindBlock.gameObject.SetActive(false);
            Sand_Trap.gameObject.SetActive(true);
        }
        if(collision.gameObject.tag=="Death_Point")
        {
            manager.Die();
        }
        if (collision.gameObject.tag == "Explosion")
        {
            manager.Die();
        }
        if(collision.gameObject.tag == "Enemy" )
        {
            if(rigid.linearVelocityY < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttck(collision.transform);

            }
            else
            {
                manager.Die();
            }
        }
        if(collision.gameObject.tag == "Snail" )
        {
            if(rigid.linearVelocityY < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttckSnail(collision.transform);

            }
            else
            {
                manager.Die();
            }
        }
        if (collision.gameObject.tag == "Spike_Turtle"||collision.gameObject.tag == "Trap")
        {
            manager.Die();

        }
        if (collision.gameObject.tag == "Hide_Block")
        {
            Hideblock.gameObject.SetActive(true);
        }
    }

    public void OnAttck(Transform enemy)
    {
        //point
        rigid.AddForce(Vector3.up * 20f, ForceMode2D.Impulse);
        if (enemy == null)
        {
            Debug.LogError("OnAttack에서 enemy가 null입니다.");
            return;
        }
        //Enemy Die
        Enemy_Move enemyMove = enemy.GetComponent<Enemy_Move>();
        enemyMove.OnDamaged();
    }
    public void OnAttckSnail(Transform snail)
    {
        //point
        rigid.AddForce(Vector3.up * 20f, ForceMode2D.Impulse);
        if (snail == null)
        {
            Debug.LogError("OnAttack에서 enemy가 null입니다.");
            return;
        }
        //Enemy Die
        Snail_Enemy snailMove = snail.GetComponent<Snail_Enemy>();
        snailMove.OnDamaged();
    }
    
    
}
