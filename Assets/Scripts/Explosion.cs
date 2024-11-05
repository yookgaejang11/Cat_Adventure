using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Explosion_Effect; // 충돌 시 생성할 새로운 오브젝트

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            // 현재 오브젝트의 위치와 회전을 저장
            Vector3 currentPosition = transform.position = new Vector3(transform.position.x, transform.position.y + 1.75f, 0);
            Quaternion currentRotation = transform.rotation;

            // 현재 오브젝트 삭제
            Destroy(gameObject);

            // 새로운 오브젝트 생성
            if (Explosion_Effect != null)
            {
                Instantiate(Explosion_Effect, currentPosition, currentRotation);
            }

        }
    }
}
