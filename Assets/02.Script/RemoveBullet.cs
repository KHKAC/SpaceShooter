using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision coll)
    {
        // if (coll.collider.tag == "BULLET")
        // if (coll.gameObject.tag == "BULLET")
        // if (coll.gameObject.tag.Equals("BULLET"))
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
        }
    }
}
