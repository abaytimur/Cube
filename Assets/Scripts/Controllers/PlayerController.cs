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

        public bool gameStarted = false;

        public Animator animator;

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

            animator = GetComponent<Animator>();
            // ragdollController = GetComponentInChildren<RagdollController>();
        }

        private void Start()
        {
            GameManager.Instance.LevelStart += LevelStarted;
            GameManager.Instance.LevelStart += LevelSuccess;
            GameManager.Instance.LevelStart += LevelFail;
        }

        private void OnDestroy()
        {
            GameManager.Instance.LevelStart -= LevelStarted;
        }

        private void LevelStarted()
        {
            gameStarted = true;
            animator.SetBool("CanRun",true);
        }

        private void LevelSuccess()
        {
        }


        private void LevelFail()
        {
        }

        private void FixedUpdate()
        {
            if (!gameStarted)
                return;

            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            
        }
    }
}