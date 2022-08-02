using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitHandler : MonoBehaviour
{
    private List<Handler> handlers = new List<Handler>();

    private void Awake()
    {
        GetComponentsInChildren(handlers);

        for (int i = 0; i < handlers.Count; i++)
        {
            handlers[i].OnAwake();
        }
    }

    private void Start()
    {
        for (int i = 0; i < handlers.Count; i++)
        {
            handlers[i].OnStart();
        }
    }
}
