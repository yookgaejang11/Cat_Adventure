using UnityEngine;

public class Shot_Bullet : MonoBehaviour
{
    public bool Player_Detacted;
    public GameObject Bullet;
    public Transform player;
    public float ShotSpeed;
    void Start()
    {
        InvokeRepeating("Shot", ShotSpeed, ShotSpeed);
    }
    private void Update()
    {
        if(Player_Detacted)
        {
            if (player != null)
            {
                // Y 좌표만 따라가고 X 좌표는 고정
                transform.position = new Vector3(transform.position.x, player.position.y+0.5f, transform.position.z);
            }
        }
    }
    private void Shot()
    {
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        Instantiate(Bullet, currentPosition, currentRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Detacted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            Player_Detacted= false;
    }




}
