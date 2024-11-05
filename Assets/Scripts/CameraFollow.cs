using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� �����մϴ�.
    public float smoothSpeed = 0.125f; // ī�޶��� �ε巯�� �̵� �ӵ�
    public Vector3 offset; // ī�޶�� �÷��̾� ������ ������

    void LateUpdate()
    {
        // �÷��̾��� ���� ��ġ�� �����ɴϴ�.
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, offset.z);
        // �ε巯�� �̵��� �����մϴ�.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
