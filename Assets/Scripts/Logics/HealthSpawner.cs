using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Logics
{
    public class HealthSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] hearths;
        private List<GameObject> _sharedHearths = new List<GameObject>();
        public List<GameObject> SharedHearths => _sharedHearths;
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            _sharedHearths.Clear();
        }

        public void SpawnHearths(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                hearths[i].SetActive(true);
                _sharedHearths.Add(hearths[i]);
            }
        }

        public void DecreaseHearths()
        {
            _health.RemoveHealth();
            GameObject temporaryObject =  _sharedHearths.LastOrDefault();
            if (temporaryObject != null) temporaryObject.SetActive(false);

            _sharedHearths.Remove(temporaryObject);
        }
    }
}
