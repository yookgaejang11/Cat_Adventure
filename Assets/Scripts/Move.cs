using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
public class PlayerMove : MonoBehaviour
{
    bool isSmear = false;
    public GameObject Sand_Trap;
    public float currentSpeed;
    SpriteRenderer spriteRenderer;
    public float MaxSpeed;
    public float Jump_power;
    Rigidbody2D rigid;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        
        
    }
    private void Update()
    {
        if(isSmear)
        {
            Jump_power = 0;
            MaxSpeed = 0;
            currentSpeed = 0;
            rigid.gravityScale = 0;
            transform.position = new Vector2(transform.position.x,transform.position.y - 0.001f);
        }
        currentSpeed = rigid.linearVelocityX; //현재 속도 계산
        //Player의 속도가 급격히 멈춤(normalized)
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocityX * 0.5f, rigid.linearVelocityY);
        }
        //Player Jump
        if (Input.GetButton("Jump") && !animator.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * Jump_power, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJump"))
        {
            Jump_power += 5;
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
        if (rigid.linearVelocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));


            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    animator.SetBool("isJump", false);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Falling_Trap")
        {
            isSmear = true;
        }
        if (collision.gameObject.tag == "Trap")
        {
            animator.SetBool("isDie", true);
        }
        if ((collision.gameObject.tag == "BushTrigger"))
        {
            Debug.Log("모래소환술");
            Sand_Trap.gameObject.SetActive(true);
        }
    }
}
