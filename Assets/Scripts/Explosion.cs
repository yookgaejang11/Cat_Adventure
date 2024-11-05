using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Explosion_Effect; // �浹 �� ������ ���ο� ������Ʈ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ������ �ִ� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���� ������Ʈ�� ��ġ�� ȸ���� ����
            Vector3 currentPosition = transform.position = new Vector3(transform.position.x, transform.position.y + 1.75f, 0);
            Quaternion currentRotation = transform.rotation;

            // ���� ������Ʈ ����
            Destroy(gameObject);

            // ���ο� ������Ʈ ����
            if (Explosion_Effect != null)
            {
                Instantiate(Explosion_Effect, currentPosition, currentRotation);
            }

        }
    }
}
