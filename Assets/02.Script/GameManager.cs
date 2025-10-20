using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // 몬스터 출현 위치 배열
    // public Transform[] points;
    public List<Transform> points = new List<Transform>();
    // 몬스터를 미리 생성해 저장할 리스트
    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxMonsters = 10;
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
    public TMP_Text scoreTxt;
    int totScore = 0;
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
        CreateMonsterPool();
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
        // 몬스터 프리펩 생성
        // Instantiate(monster, points[idx].position, points[idx].rotation);

        // 오브젝트 풀에서 몬스터 추출
        GameObject _monster = GetMonsterInPool();
        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        _monster?.SetActive(true);
    }

    void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonsters; i++)
        {
            // 몬스터 생성
            var _monster = Instantiate<GameObject>(monster);
            // 몬스터의 이름을 지정
            //_monster.name = "Monster" + i.ToString("00");
            _monster.name = $"Monster_{i:00}";
            // 몬스터 비활성화
            _monster.SetActive(false);
            // 생성한 몬스터를 오브젝트 풀에 추가        
            monsterPool.Add(_monster);
        }
    }

    // 오브젝트 풀에서 사용가능한 몬스터를 추출해 반환하는 함수
    public GameObject GetMonsterInPool()
    {
        foreach (var _monster in monsterPool)
        {
            if (_monster.activeSelf == false)
            {
                return _monster;
            }
        }
        return null;
    }
    
    public void DisPlayScore(int score)
    {
        totScore += score;
        scoreTxt.text = $"<color=#00ff00>SCORE :</color> <color=#ff0000>{totScore:#,##0}</color>";
    }
}
