using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    const int HIT_COUNT = 3; // 폭발에 필효한 hit 수
    const float DESTROY_EXPLOSION = 5.0f; // 폭발 이펙트 유지 시간
    const float DESTROY_BARREL = 3.0f; // 폭발 후 오브젝트 유지 시간
    const float BARREL_MASS = 1.0f; // 오브젝트 무게 일시 변경
    const float UP_FORCE = 1500.0f; // 튀어오르기 효과를 위한 AddForce 힘
    [SerializeField] GameObject explosionEffect;

    #region private
    Transform tr;
    Rigidbody rb;
    int hitCount = 0;
    #endregion

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // 충돌 시 발생
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == HIT_COUNT)
            {
                ExplosionBarrel();
            }
        }
    }

    void ExplosionBarrel()
    {
        //폭파 효과 파티클 생성
        GameObject explosion = Instantiate(explosionEffect, tr.position, Quaternion.identity);
        // 파티클 생성 5초후 파티클  제거
        Destroy(explosion, DESTROY_EXPLOSION);
        // Barrel의 무게를 가볍게
        rb.mass = BARREL_MASS;
        // 위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * UP_FORCE);
        // 3초 후 Barrel 제거
        Destroy(gameObject, DESTROY_BARREL);
    }
}
