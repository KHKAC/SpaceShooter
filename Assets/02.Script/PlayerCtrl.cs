using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // component cash
    [SerializeField] Transform tr;
    [SerializeField] float moveSpeed = 10.0f;
    void Start()
    {
        // GetComponent
        // tr = this.gameObject.GetComponent<Transform>();
        // GetComponent("Transform") as Transform;
        // tr = (Transform)GetComponent((typeof(Transform)))
        // 실제로 사용하는 것.
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Debug.Log($"h = {h}");
        Debug.Log($"v = {v}");
        // Transform.Position과 normalized Vector의 표현 방법 차이
        // transform.position += new Vector3(0, 0, 1); // Transform.Position
        // tr.position += Vector3.forward * 1; // normalized Vector
        tr.Translate(Vector3.forward * Time.deltaTime * v * moveSpeed, Space.Self);
    }
}
