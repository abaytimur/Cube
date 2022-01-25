using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Logics
{
    public class HearthSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] hearths;
        private List<GameObject> _sharedHearths = new List<GameObject>();
        public List<GameObject> SharedHearth => _sharedHearths;
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

        public void SuccessPanelHearths()
        {
            for (int i = 0; i < hearths.Length; i++)
            {
                hearths[i].SetActive(false);
                _sharedHearths.Clear();
            }

            for (int i = 0; i < _health.HealthData.maxHealth; i++)
            {
                _sharedHearths.Add(hearths[i]);
                hearths[i].SetActive(true);
            }
        }

        public void OpenNewHearthImage()
        {
            for (int i = 0; i < hearths.Length; i++)
            {
                if (hearths[i] == _sharedHearths.LastOrDefault())
                {
                    if (hearths[i+1] != null)
                    {
                        hearths[i + 1].SetActive(true);
                    }
                }
            }
        }

        public void DecreaseHearths()
        {
            _sharedHearths.Clear();
            foreach (var item in hearths)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    _sharedHearths.Add(item);
                }
            }
            _health.RemoveHealth();
            var temporaryObject = _sharedHearths.LastOrDefault()?.gameObject;
            if (temporaryObject != null)
            {
                temporaryObject.SetActive(false);
                _sharedHearths.Remove(temporaryObject);
            }
        }
    }
}