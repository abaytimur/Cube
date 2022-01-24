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

        [SerializeField] private GameObject levelStartCanvas;
        [SerializeField] private GameObject successCanvas;
        [SerializeField] private GameObject failCanvas;
        [SerializeField] private GameObject gameplayCanvas;
        [SerializeField] private GameObject levelNumberText;

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
        }

        private void Start()
        {
            GameManager.Instance.LevelStart += LevelStarted;
            GameManager.Instance.LevelFail += LevelFailed;
            GameManager.Instance.LevelSuccess += LevelSucceeded;

            levelNumberText.GetComponent<TextMeshProUGUI>().text = $"LEVEL {SceneManager.GetActiveScene().buildIndex + 1}";
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
            gameplayCanvas.gameObject.SetActive(false);
            StartCoroutine(DelayedSuccess());
        }

        private IEnumerator DelayedSuccess()
        {
            yield return new WaitForSeconds(.5f);
            successCanvas.gameObject.SetActive(true);
        }

        private void LevelFailed()
        {
            gameplayCanvas.gameObject.SetActive(false);
            failCanvas.gameObject.SetActive(true);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
