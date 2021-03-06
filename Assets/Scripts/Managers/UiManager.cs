using System;
using System.Collections;
using Controllers;
using Logics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        private static UiManager _instance;
        public static UiManager Instance => _instance;

        [SerializeField] private GameObject levelStartCanvas;
        [SerializeField] private GameObject successCanvas;
        [SerializeField] private GameObject failCanvas;
        [SerializeField] private GameObject gameplayCanvas;
        [SerializeField] private GameObject levelNumberText;

        [SerializeField] private GameObject gameplayGoldCoinBox;
        private TextMeshProUGUI _gameplayGoldCoinAmount;
        public TextMeshProUGUI GameplayGoldCoinAmount => _gameplayGoldCoinAmount;

        [SerializeField] private GameObject playerGoldCoinBox;
        public TextMeshProUGUI playerGoldCoinAmount;

       [SerializeField] private HearthSpawner hearthSpawner;
        public HearthSpawner HearthSpawner => hearthSpawner;

        private int _levelIndex;

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

            successCanvas.gameObject.SetActive(false);
            failCanvas.gameObject.SetActive(false);
            _gameplayGoldCoinAmount = gameplayGoldCoinBox.GetComponentInChildren<TextMeshProUGUI>();
            playerGoldCoinAmount = playerGoldCoinBox.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            GameManager.Instance.LevelStart += LevelStarted;
            GameManager.Instance.LevelFail += LevelFailed;
            GameManager.Instance.LevelSuccess += LevelSucceeded;

            levelStartCanvas.SetActive(true);
            gameplayGoldCoinBox.SetActive(false);

            _levelIndex = PlayerPrefs.GetInt("LevelIndex");
            
            levelNumberText.GetComponent<TextMeshProUGUI>().text = $"LEVEL {_levelIndex+1}";
            _gameplayGoldCoinAmount.text = "0";
            
            
        }

        private void OnDestroy()
        {
            GameManager.Instance.LevelStart -= LevelStarted;
            GameManager.Instance.LevelFail -= LevelFailed;
            GameManager.Instance.LevelSuccess -= LevelSucceeded;
        }

        private void LevelStarted()
        {
            levelStartCanvas.SetActive(false);
            playerGoldCoinBox.SetActive(false);
            gameplayGoldCoinBox.SetActive(true);
        }

        private void LevelSucceeded()
        {
            gameplayCanvas.gameObject.SetActive(true);
            playerGoldCoinBox.SetActive(true);
            StartCoroutine(DelayedSuccess());
        }

        private IEnumerator DelayedSuccess()
        {
            yield return new WaitForSeconds(3f);
            HearthSpawner.SuccessPanelHearths();
            
            successCanvas.gameObject.SetActive(true);
            CollectGameplayCoin();
        }

        private void CollectGameplayCoin()
        {
            int temporaryCoinAmount = Convert.ToInt32(_gameplayGoldCoinAmount.text);
            if (temporaryCoinAmount > 0)
            {
                // adding gameplay coins to player coins
                PlayerController.Instance.Gold.AddGold(temporaryCoinAmount);
                _gameplayGoldCoinAmount.text = "0";
            }
        }

        private void LevelFailed()
        {
            StartCoroutine(DelayedFail());
        }

        private IEnumerator DelayedFail()
        {
            yield return new WaitForSeconds(3f);
            gameplayCanvas.gameObject.SetActive(false);
            failCanvas.gameObject.SetActive(true);
        }
    }
}