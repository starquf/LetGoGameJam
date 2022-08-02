using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager<T>
{
    // 이벤트 매니져 v.2

    // 이제 키값으로 문자열 말고도 여러가지 사용 할 수 있음
    // Action으로 관리함
    // 단, 씬 바꿀 때마다 사용한 해당 이벤트 매니저는 꼭 초기화 시켜야함

    private static Dictionary<T, Action> events = new Dictionary<T, Action>();

    public static void AddEvent(T key, Action action)    // 특정 이름의 이벤트를 추가하고 함수도 같이 추가
    {
        if (!events.ContainsKey(key))
        {
            events.Add(key, () => { });
        }

        events[key] += action;
    }

    public static void Invoke(T key)
    {
        if (events.ContainsKey(key))
        {
            events[key].Invoke();
        }
    }

    public static void RemoveEvent(T key, Action action) // 특정 이름의 이벤트 안에 있는 함수를 지움
    {
        if (events.ContainsKey(key))
        {
            events[key] -= action;
        }
    }

    public static void RemoveAllEvents(T key)            // 특정 이름의 이벤트를 초기화
    {
        if (events.ContainsKey(key))
        {
            events[key] = () => { };
        }
    }

    public static void RemoveAllEvents()                 // 딕셔너리 안에 있는 이벤트를 싹다 초기화
    {
        List<T> keys = new List<T>();

        foreach (var key in events.Keys)
        {
            keys.Add(key);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            events[keys[i]] = () => { };
        }
    }

    public static void PrintKey()
    {
        Debug.Log($"===== {typeof(T).ToString()} 이벤트 매니저 목록 =====");

        foreach (var key in events.Keys)
        {
            Debug.Log(key.ToString());
        }
    }
}
