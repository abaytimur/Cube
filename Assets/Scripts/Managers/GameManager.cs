using System;
using Logics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action LevelStart;
        public event Action LevelFail;
        public event Action LevelSuccess;
        public event Action LevelRestart;


        private static GameManager _instance;
        public static GameManager Instance => _instance;

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

            Application.targetFrameRate = 60;

#if UNITY_EDITOR
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
#endif
        }

        public void OnLevelStart()
        {
            print("Level Started");

            LevelStart?.Invoke();
        }

        public void OnLevelFail()
        {
            print("Level Failed");

            LevelFail?.Invoke();
        }

        public void OnLevelSuccess()
        {
            print("Level Success");

            LevelSuccess?.Invoke();
        }

        public void RestartLevel()
        {
            OnLevelRestart();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel()
        {
           
            var temporaryInt = PlayerPrefs.GetInt("LevelIndex");
            PlayerPrefs.SetInt("LevelIndex", temporaryInt + 1);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        protected virtual void OnLevelRestart()
        {
            print("Level Restart");

            LevelRestart?.Invoke();
        }

    }
}