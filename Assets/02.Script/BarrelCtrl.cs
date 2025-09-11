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
    const float OVER_FORCE = 1200.0f;

    #region private
    [SerializeField] GameObject explosionEffect;
    [SerializeField] Texture[] textures;
    [SerializeField] float radius = 10.0f;
    new MeshRenderer mr;
    Transform tr;
    Rigidbody rb;
    int hitCount = 0;
    Collider[] colls = new Collider[10];
    #endregion

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        // GetComponentInChildren 오브젝트 하위에 있는 컴포넌트 확인
        mr = GetComponentInChildren<MeshRenderer>();

        // 난수 생성(Random.Range())
        int barrelIndex = Random.Range(0, textures.Length);
        //float a = Random.Range(0f, 4f);
        // 텍스쳐 지정
        mr.material.mainTexture = textures[barrelIndex];
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

        // // Barrel의 무게를 가볍게
        // rb.mass = BARREL_MASS;
        // // 위로 솟구치는 힘을 가함
        // rb.AddForce(Vector3.up * UP_FORCE);

        // 간접 폭발력 적용
        IndirectDamage(tr.position);
        // 3초 후 Barrel 제거
        Destroy(gameObject, DESTROY_BARREL);
    }

    void IndirectDamage(Vector3 pos)
    {
        // 주변에 있는 드럼통을 모두 추출
        // GC 발생
        // Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 3);
        Physics.OverlapSphereNonAlloc(pos, radius, colls, 1 << 3);
        foreach (var item in colls)
        {
            if (item == null) continue;
            rb = item.GetComponent<Rigidbody>();
            rb.mass = BARREL_MASS;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(UP_FORCE, pos, radius, OVER_FORCE);
        }
    }
}
