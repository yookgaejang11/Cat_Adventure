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
            Debug.LogError("AudioSource를 찾을 수 없습니다. 이 오브젝트에 AudioSource 컴포넌트를 추가하세요.");
        }
        Stage1Bgm.loop = true;
        Stage1Bgm.Play();
        
        manager = FindFirstObjectByType<GameManager>();// GameManager가 장면에 있으면 할당됩니다.
        if (manager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없습니다. 씬에 GameManager가 있는지 확인하세요.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("죽음"); 
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
