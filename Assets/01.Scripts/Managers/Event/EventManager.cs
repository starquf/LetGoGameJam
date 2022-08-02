using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager<T>
{
    // �̺�Ʈ �Ŵ��� v.2

    // ���� Ű������ ���ڿ� ���� �������� ��� �� �� ����
    // Action���� ������
    // ��, �� �ٲ� ������ ����� �ش� �̺�Ʈ �Ŵ����� �� �ʱ�ȭ ���Ѿ���

    private static Dictionary<T, Action> events = new Dictionary<T, Action>();

    public static void AddEvent(T key, Action action)    // Ư�� �̸��� �̺�Ʈ�� �߰��ϰ� �Լ��� ���� �߰�
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

    public static void RemoveEvent(T key, Action action) // Ư�� �̸��� �̺�Ʈ �ȿ� �ִ� �Լ��� ����
    {
        if (events.ContainsKey(key))
        {
            events[key] -= action;
        }
    }

    public static void RemoveAllEvents(T key)            // Ư�� �̸��� �̺�Ʈ�� �ʱ�ȭ
    {
        if (events.ContainsKey(key))
        {
            events[key] = () => { };
        }
    }

    public static void RemoveAllEvents()                 // ��ųʸ� �ȿ� �ִ� �̺�Ʈ�� �ϴ� �ʱ�ȭ
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
        Debug.Log($"===== {typeof(T).ToString()} �̺�Ʈ �Ŵ��� ��� =====");

        foreach (var key in events.Keys)
        {
            Debug.Log(key.ToString());
        }
    }
}
