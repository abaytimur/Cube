using System;
using System.Collections;
using Controllers;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logics
{
    public class Item : MonoBehaviour, IPooledObject
    {
        [SerializeField] Vector3 targetRotation = default;

        private Camera _camera;
        private Transform _sidePanel;

        private bool _move;

        private void Awake()
        {
            _camera = Camera.main;
            _sidePanel = UiManager.Instance.GameplayGoldCoinAmount.gameObject.transform;
        }

        private void FixedUpdate()
        {
            if (!_move)
                return;
            
            Vector3 target =
                _camera.ScreenToWorldPoint(new Vector3(_sidePanel.position.x, _sidePanel.position.y, 10));
            transform.position = Vector3.MoveTowards(transform.position, target, 20 * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation),
                20 * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * 0.6f, 20 * Time.deltaTime);

            if (transform.position == target)
            {
                UiManager.Instance.GameplayGoldCoinAmount.text =
                    $"{Convert.ToInt32(UiManager.Instance.GameplayGoldCoinAmount.text) + 1}";
                gameObject.SetActive(false);
            }
        }

        public void OnObjectSpawn()
        {
            StartCoroutine(DelayedMove());
        }

        private IEnumerator DelayedMove()
        {
            yield return null;
            // yield return new WaitForSeconds(Random.Range(.7f, 1f));
            Move();
        }

        public void Move()
        {
            _move = true;
        }
    }
}