using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform targetTr;
    Transform camTr;

    [Range(2.0f, 20.0f)] public float distance = 10.0f;
    [Range(0.0f, 10.0f)] public float height = 2.0f;

    public float damping = 10.0f;
    public float targerOffset = 2.0f;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Main Camera 자신의 Transform
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // 추적해야 할 대상의 뒤쪽으로 distance 이동
        // 높이를 height 만큼 이동
        Vector3 pos  = targetTr.position + (-targetTr.forward * distance) + (Vector3.up * height);

        // 구면 선형 보간 함수를 사용해 부드럽게 위치를 변경
        // camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);

        // Camera를 피벗 좌표를 향해 회전
        camTr.LookAt(targetTr.position + (targetTr.up * targerOffset));
    }
}
