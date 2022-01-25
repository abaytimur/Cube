using Managers;
using UnityEngine;
using Utilities;

namespace Logics
{
    public class Health : MonoBehaviour
    {
        private HealthData _healthData;

        private void Start()
        {
            LoadData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddHealth();
            }
        }

        public void AddHealth()
        {
            _healthData.maxHealth++;
            if (_healthData.maxHealth >=7)
            {
                // 7 is max health
                _healthData.maxHealth = 7;
            }
            // todo: panelde goster
        }
        
        public void RemoveHealth()
        {
            _healthData.amount--;
            if (_healthData.amount<=0)
            {
                _healthData.amount = 0;
            }
            print("Health.cs, health.data: "+ _healthData.amount);
            // todo: panelde goster
        }


        private void SaveData()
        {
            SaveManager.SaveData(_healthData, "health");

            Debug.Log($"Saved health amount: {_healthData.amount}");
            Debug.Log($"Saved maxHealth amount: {_healthData.maxHealth}");
        }

        private void LoadData()
        {
            _healthData = SaveManager.LoadData<HealthData>("health");
            if (_healthData == null)
            {
                _healthData = new HealthData();
                _healthData.maxHealth = 3;
                _healthData.amount = _healthData.maxHealth;
            }
            else
            {
                _healthData.amount = _healthData.maxHealth;

            }

            UiManager.Instance.HearthSpawner.SpawnHearths(_healthData.amount);
            Debug.Log($"Loaded health amount: {_healthData.amount}");
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                SaveData();
        }
    }
}