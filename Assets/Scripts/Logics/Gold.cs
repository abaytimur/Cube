using System;
using Managers;
using UnityEngine;
using Utilities;

namespace Logics
{
    public class Gold : MonoBehaviour
    {
        private GoldData _goldData;
        public GoldData GoldData => _goldData;

        private void Start()
        {
            GameManager.Instance.LevelRestart += LevelRestart;
            LoadData();
        }
        
        public void AddGold(int amount)
        {
            _goldData.amount += amount;

            UiManager.Instance.playerGoldCoinAmount.text = $"{_goldData.amount}";
            print("New gold amount: " + _goldData.amount);
        }

        public void RemoveGold(int amount)
        {
            _goldData.amount -= amount;

            UiManager.Instance.playerGoldCoinAmount.text = $"{_goldData.amount}";
        }

        private void SaveData()
        {
            SaveManager.SaveData(_goldData, "gold");

            Debug.Log($"Saved gold amount: {_goldData.amount}");
        }

        private void LoadData()
        {
            _goldData = SaveManager.LoadData<GoldData>("gold");
            if (_goldData == null)
                _goldData = new GoldData();

            UiManager.Instance.playerGoldCoinAmount.text = $"{_goldData.amount}";

            Debug.Log($"Loaded gold amount: {_goldData.amount}");
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