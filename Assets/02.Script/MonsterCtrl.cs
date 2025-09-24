using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    const float TIME_WAIT = 0.3f;
    public enum MonState { IDLE, TRACE, ATTACK, DIE }
    public MonState monState = MonState.IDLE;
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
    public bool isDie = false;

    //Animator parameter Hash 값 추출
    readonly int hashTrace = Animator.StringToHash("IsTrace");
    readonly int hashAttack = Animator.StringToHash("IsAttack");
    readonly int hashHit = Animator.StringToHash("Hit");
    readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");
    readonly int hashAnimSpeed = Animator.StringToHash("Speed");

    Transform monsterTr;
    Transform playerTr;
    NavMeshAgent agent;
    GameObject bloodEffect;
    Animator anim;

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        // agent.destination = playerTr.position;
        // agent.SetDestination(playerTr.position);

        bloodEffect = Resources.Load<GameObject>("BloodSprayEffect");
        // 몬스터의 상태를 체크하는 코루틴
        StartCoroutine(CheckMonState());
        // 상태에 따른 몬스터의 행동을 수행하는 코루틴
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(TIME_WAIT);
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);
            if (distance <= attackDist)
            {
                monState = MonState.ATTACK;
            }
            else if (distance <= traceDist)
            {
                monState = MonState.TRACE;
            }
            else
            {
                monState = MonState.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monState)
            {
                case MonState.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;
                case MonState.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case MonState.ATTACK:
                    anim.SetBool(hashAttack, true);
                    break;
                case MonState.DIE:
                    break;
            }
            yield return new WaitForSeconds(TIME_WAIT);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            Destroy(collision.gameObject);
            anim.SetTrigger(hashHit);

            Vector3 pos = collision.GetContact(0).point;
            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);

            ShowBloodEffect(pos, rot);
        }
    }

    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot, monsterTr);
        Destroy(blood, 1.0f);
    }

    void OnDrawGizmos()
    {
        if (monState == MonState.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);
        }

        if (monState == MonState.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.gameObject.name);
    // }

    // Player에서 string 형태로 부르고 있음
    void OnPlayerDie()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        anim.SetFloat(hashAnimSpeed, Random.Range(0.8f, 1.2f));
        anim.SetTrigger(hashPlayerDie);
    }
}
