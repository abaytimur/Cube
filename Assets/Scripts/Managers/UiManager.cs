using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
       private static UiManager _instance;
        public static UiManager Instance => _instance;

        // [SerializeField] private GameObject levelStartCanvas;
        [SerializeField] private GameObject _successPanel;
        [SerializeField] private GameObject _failPanel;
        [SerializeField] private GameObject _gameplayPanel;
        [SerializeField] private GameObject _levelNumberText;

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

            _successPanel.gameObject.SetActive(false);
            _failPanel.gameObject.SetActive(false);
        }

        private void Start()
        {
            GameManager.Instance.LevelStart += LevelStarted;
            GameManager.Instance.LevelFail += LevelFailed;
            GameManager.Instance.LevelSuccess += LevelSucceeded;

            _levelNumberText.GetComponent<TextMeshProUGUI>().text = $"LEVEL {SceneManager.GetActiveScene().buildIndex + 1}";
        }

        private void OnDestroy()
        {
            GameManager.Instance.LevelStart -= LevelStarted;
            GameManager.Instance.LevelFail -= LevelFailed;
            GameManager.Instance.LevelSuccess -= LevelSucceeded;
        }
        
        private void LevelStarted()
        {
            // levelStartCanvas.SetActive(false);
        }

        private void LevelSucceeded()
        {
            _gameplayPanel.gameObject.SetActive(false);
            StartCoroutine(DelayedSuccess());
        }

        private IEnumerator DelayedSuccess()
        {
            yield return new WaitForSeconds(.5f);
            _successPanel.gameObject.SetActive(true);
        }

        private void LevelFailed()
        {
            _gameplayPanel.gameObject.SetActive(false);
            _failPanel.gameObject.SetActive(true);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
