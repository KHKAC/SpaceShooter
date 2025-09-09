using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    [SerializeField] GameObject sparkEffect;
    void OnCollisionEnter(Collision coll)
    {
        // if (coll.collider.tag == "BULLET")
        // if (coll.gameObject.tag == "BULLET")
        // if (coll.gameObject.tag.Equals("BULLET"))
        if (coll.collider.CompareTag("BULLET"))
        {
            ContactPoint cp = coll.GetContact(0);
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
            Destroy(spark, 0.5f);
            Destroy(coll.gameObject);
        }
    }
}
