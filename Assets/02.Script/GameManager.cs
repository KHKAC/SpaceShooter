using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // 몬스터 출현 위치 배열
    // public Transform[] points;
    public List<Transform> points = new List<Transform>();
    public GameObject monster;
    public float createTime = 3.0f;
    // isGameOver를 Property로 만들기
    bool isGameOver;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    // 싱글톤 형태로 만드는 것
    public static GameManager Instance = null;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
        int idx = Random.Range(0, points.Count);
        Instantiate(monster, points[idx].position, points[idx].rotation);
    }
}
