using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 참조합니다.
    public float smoothSpeed = 0.125f; // 카메라의 부드러운 이동 속도
    public Vector3 offset; // 카메라와 플레이어 사이의 오프셋

    void LateUpdate()
    {
        // 플레이어의 현재 위치를 가져옵니다.
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, offset.z);
        // 부드러운 이동을 적용합니다.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
