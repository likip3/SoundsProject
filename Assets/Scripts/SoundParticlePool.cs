using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundParticlePool : MonoBehaviour
{
    public static SoundParticlePool Instance;

    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjectType
        {
            SoundParticle,
            SoundWave,
        }

        public ObjectType Type;
        public GameObject gameObject;
        public int StartCount; 

    }

    [SerializeField] private List<ObjectInfo> objectInfos;

    private Dictionary<ObjectInfo.ObjectType, Pool> pools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        InitPool();
    }

    private void InitPool()
    {
        pools = new Dictionary<ObjectInfo.ObjectType, Pool>();
        var emptyGameObject = new GameObject();

        foreach (var obj in objectInfos)
        {
            var container = Instantiate(emptyGameObject, transform, false);
            container.name = obj.Type.ToString();

            pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var go = InstantiateObject(obj.Type,container.transform);
                pools[obj.Type].objects.Enqueue(go);

            }
        }
        Destroy(emptyGameObject);
    }

    private GameObject InstantiateObject(ObjectInfo.ObjectType type, Transform parent)
    {
        var go = Instantiate(objectInfos.Find(x => x.Type == type).gameObject, parent);
        go.SetActive(false);
        return go;
    }

    public GameObject GetObject(ObjectInfo.ObjectType type)
    {
        var obj = pools[type].objects.Count > 0
            ? pools[type].objects.Dequeue()
            : InstantiateObject(type, pools[type].Container);
        obj.SetActive(true);
        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        pools[obj.GetComponent<IPooledObject>().Type].objects.Enqueue(obj);
        obj.SetActive(false);
    }
}
