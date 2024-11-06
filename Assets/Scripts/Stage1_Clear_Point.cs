using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Stage1_Clear_Point : MonoBehaviour
{
    public int Speed;
    bool isClear;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isClear)
        {
            rigid.AddForce(Vector2.right,ForceMode2D.Impulse);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isClear = true;
            Invoke("NextStage", 3f);
        }
    }
    void NextStage()
    {
        SceneManager.LoadScene("Stage2");
    }
}
