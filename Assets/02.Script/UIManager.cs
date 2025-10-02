using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button optionBtn;
    [SerializeField] Button shopBtn;

    UnityAction action;

    void Start()
    {
        // Unity Action 사용
        action = () => OnButtonClick(startBtn.name);
        startBtn.onClick.AddListener(action);

        // 무명 메서드 사용
        optionBtn.onClick.AddListener(delegate { OnButtonClick(optionBtn.name); });

        // 람다식 사용
        shopBtn.onClick.AddListener(() => OnButtonClick(shopBtn.name));
    }

    public void OnButtonClick(string message)
    {
        Debug.Log($"Click button: {message}");
    }
}
