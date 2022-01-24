using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance => _instance;

        public float movementSpeed = 5f;

        [SerializeField] private bool _canMove = false;

        private Animator _animator;

        // [HideInInspector] public RagdollController ragdollController;

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

            _animator = GetComponent<Animator>();
            // ragdollController = GetComponentInChildren<RagdollController>();
        }

        private void Start()
        {
            GameManager.Instance.LevelStart += LevelStarted;
            GameManager.Instance.LevelSuccess += LevelSuccess;
            GameManager.Instance.LevelFail += LevelFail;
        }

        private void OnDestroy()
        {
            GameManager.Instance.LevelStart -= LevelStarted;
            GameManager.Instance.LevelSuccess -= LevelSuccess;
            GameManager.Instance.LevelFail -= LevelFail;
        }

        private void LevelStarted()
        {
            _canMove = true;
            _animator.SetBool("CanRun", true);
        }

        private void LevelSuccess()
        {
            _canMove = false;
            _animator.SetBool("Win", true);
        }


        private void LevelFail()
        {
            _canMove = false;
            _animator.SetBool("CanRun", false);
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;

            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }
}