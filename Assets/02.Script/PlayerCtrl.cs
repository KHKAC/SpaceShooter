using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    const float TIME_INTER = 0.10f;
    const float INPUT_VALUE = 0.05f;
    const float INIT_HP = 100.0f;
    const float PUNCH_POWER = 10.0f;
    // component cash
    Transform tr;
    Animation anim;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float turnSpeed = 500.0f;

    public float currHP;

    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie; // event 선언 시 변수 이름 앞에 On을 붙인다.

    IEnumerator Start()
    {
        currHP = INIT_HP;
        // GetComponent
        // tr = this.gameObject.GetComponent<Transform>();
        // GetComponent("Transform") as Transform;
        // tr = (Transform)GetComponent((typeof(Transform)))
        // 실제로 사용하는 것.
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.Play("Idle");
        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.5f);
        turnSpeed = 500.0f;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // Transform.Position과 normalized Vector의 표현 방법 차이
        // transform.position += new Vector3(0, 0, 1); // Transform.Position
        // tr.position += Vector3.forward * 1; // normalized Vector
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= INPUT_VALUE)
        {
            anim.CrossFade("RunF", TIME_INTER);
        }
        else if (v <= -INPUT_VALUE)
        {
            anim.CrossFade("RunB", TIME_INTER);
        }
        else if (h >= INPUT_VALUE)
        {
            anim.CrossFade("RunR", TIME_INTER);
        }
        else if (h <= -INPUT_VALUE)
        {
            anim.CrossFade("RunL", TIME_INTER);
        }
        else
        {
            anim.CrossFade("Idle", TIME_INTER);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currHP > 0.0f && other.CompareTag("PUNCH"))
        {
            currHP -= PUNCH_POWER;
            Debug.Log($"Player HP = {currHP / INIT_HP * 100}%");
            if (currHP <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    // 주인공 사망 이벤트 호출(발생)
    void PlayerDie()
    {
        Debug.Log("Player Die");
        // GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        // foreach (GameObject monster in monsters)
        // {
        //     monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        // }

        OnPlayerDie();
    }
}
