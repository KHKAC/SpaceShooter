using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 몬스터 출현 위치 배열
    // public Transform[] points;
    public List<Transform> points = new List<Transform>();
    public GameObject monster;
    public float createTime = 3.0f;
    bool isGameOver;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if(isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }
    void Start()
    {
        // SpawnPointGroup 게임오브젝트의 Transform Component 추출
        /*
        GameObject go = GameObject.Find("SpawnPointGroup");
        if(go != null)
        {
            Transform spg = go.transform;
            if(spg != null)
            {
                points = spg.GetComponentsInChildren<Transform>();
            }
        }
        */
        Transform spg = GameObject.Find("SpawnPointGroup")?.transform;
        // points = spawnPointGroup?.GetComponentsInChildren<Transform>();
        // spg?.GetComponentsInChildren<Transform>(points);
        foreach(Transform point in spg)
        {
            points.Add(point);
        }
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        int idx = Random.Range(1, points.Count);
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }
}
