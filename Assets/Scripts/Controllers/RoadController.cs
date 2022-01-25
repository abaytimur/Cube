using System.Collections.Generic;
using Managers;
using UnityEngine;
using Utilities;

namespace Controllers
{
    public class RoadController : MonoBehaviour
    {
        private static RoadController _instance;
        public static RoadController Instance => _instance;

        public int InitialRoadCount { get; private set; } = 15;
        [SerializeField] private int totalRoadCount = 20;
        public int TotalRoadCount => totalRoadCount;

        private List<GameObject> _roadList = new List<GameObject>();
        public List<GameObject> RoadList => _roadList;

        [HideInInspector] public bool isEndRoadSpawned = false;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _roadList.Clear();

            for (int i = 0; i < InitialRoadCount; i++)
            {
                var tempObject = ObjectPooler.Instance.SpawnFromPool(Extensions.RoadName,
                    transform.position + (Vector3.forward * i) * Extensions.RoadOffset, Quaternion.identity);
                _roadList.Add(tempObject);
            }
        }
    }
}