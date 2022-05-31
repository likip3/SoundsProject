using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Queue<GameObject> objects;

    public Pool(Transform container)
    {
        Container = container;
        objects = new Queue<GameObject>();
    }

    public Transform Container { get; }
}