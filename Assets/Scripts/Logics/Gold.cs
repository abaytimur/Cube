using System;
using Managers;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using Utilities;

namespace Logics
{
    public class Gold: MonoBehaviour
    {
        private void Start()
        {
            LoadData();
            Debug.Log(Application.persistentDataPath);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddGold(+45);
            }
        }

        private GoldData _goldData;

        public void AddGold(int amount)
        {
            _goldData.amount += amount;
            // todo: panelde goster
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
            
            // todo: panelde goster
            
            Debug.Log($"Loaded gold amount: {_goldData.amount}");
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