using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateDemo : MonoBehaviour
{
    //delegate 선언
    delegate float SumHandler(float a, float b);
    //delegate type 변수 선언
    SumHandler sumHandler;

    float Sum(float a, float b)
    {
        return a + b;
    }

    void Start()
    {
        //delegate 벼수에 메서드 할당
        sumHandler = Sum;
        //delegate 실행
        float sum = sumHandler(10.0f, 5.0f);
        //결과값 출력
        Debug.Log($"Sum = {sum}");
        sumHandler = (float a, float b) => (a + b);
        float sum2 = sumHandler(10.0f, 0.5f);
        Debug.Log($"sum2 = {sum2}");

        sumHandler = delegate (float a, float b) { return a + b; };
        float sum3 = sumHandler(2.0f, 3.0f);
        Debug.Log($"sum3 = {sum3}");
    }
}
