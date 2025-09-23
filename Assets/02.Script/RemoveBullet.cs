using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    [SerializeField] GameObject sparkEffect;
    [SerializeField] Transform bulletEffectTr;
    void OnCollisionEnter(Collision coll)
    {
        // if (coll.collider.tag == "BULLET")
        // if (coll.gameObject.tag == "BULLET")
        // if (coll.gameObject.tag.Equals("BULLET"))
        if (coll.collider.CompareTag("BULLET"))
        {
            // 충돌 지점
            ContactPoint cp = coll.GetContact(0);
            // 법선 벡터를 쿼터니언 타입으로 변경
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            // 스파크 생성
            GameObject spark = Instantiate(sparkEffect, cp.point, rot, bulletEffectTr);
            
            Destroy(spark, 0.5f);
            Destroy(coll.gameObject);
        }
    }
}
