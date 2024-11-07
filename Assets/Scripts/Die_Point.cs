using UnityEngine;

public class Die_Point : MonoBehaviour
{
    bool isDie;
    AudioSource Stage1Bgm;
    GameManager manager;

    private void Awake()
    {
        Stage1Bgm = GetComponent<AudioSource>();
        isDie = false;
        if (Stage1Bgm == null)
        {
            Debug.LogError("AudioSource�� ã�� �� �����ϴ�. �� ������Ʈ�� AudioSource ������Ʈ�� �߰��ϼ���.");
        }
        Stage1Bgm.loop = true;
        Stage1Bgm.Play();
        
        manager = FindFirstObjectByType<GameManager>();// GameManager�� ��鿡 ������ �Ҵ�˴ϴ�.
        if (manager == null)
        {
            Debug.LogError("GameManager�� ã�� �� �����ϴ�. ���� GameManager�� �ִ��� Ȯ���ϼ���.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("����"); 
            isDie = true;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (isDie)
        {
            
            manager.Die();
            
        }
    }




}
