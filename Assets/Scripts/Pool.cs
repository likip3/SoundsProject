using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public Transform Container { get; }
    public Queue<GameObject> objects;
    public Pool(Transform container)
    {
        Container = container;
        objects = new Queue<GameObject>();
    }
}
