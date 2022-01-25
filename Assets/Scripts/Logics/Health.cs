using Controllers;
using Managers;
using UnityEngine;
using Utilities;

namespace Logics
{
    public class Health : MonoBehaviour
    {
        private HealthData _healthData;
        [SerializeField] private int healthBuyCost;
        
        private void Start()
        {
            GameManager.Instance.LevelRestart += LevelRestart;
            LoadData();
        }

        public void AddHealth()
        {
            if (PlayerController.Instance.Gold.GoldData.amount >= healthBuyCost && _healthData.maxHealth < 7)
            {
                PlayerController.Instance.Gold.RemoveGold(healthBuyCost);
                
                _healthData.maxHealth++;
                UiManager.Instance.HearthSpawner.SpawnHearths(_healthData.amount);
            }
        }
        
        public void RemoveHealth()
        {
            _healthData.amount--;
            if (_healthData.amount<=0)
            {
                _healthData.amount = 0;
                
                PlayerController.Instance.RagdollController.Die();
                GameManager.Instance.OnLevelFail();
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

        
        private void LevelRestart()
        {
            SaveData();
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