using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public interface IPooledObject
    {
        void OnObjectSpawn();
    }

    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int size;
        }

        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        public static ObjectPooler Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            for (int i = 0, size = pools.Count; i < size; i++)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int k = 0, poolSize = pools[i].size; k < poolSize; k++)
                {
                    GameObject obj = Instantiate(pools[i].prefab, transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pools[i].tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
                return null;

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObj != null)
                pooledObj.OnObjectSpawn();

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        public GameObject SpawnPropsFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!poolDictionary.ContainsKey(tag))
                return null;

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.parent = parent;
            objectToSpawn.transform.localPosition = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
            if (pooledObj != null)
                pooledObj.OnObjectSpawn();

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
    }
}