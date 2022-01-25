using System.Collections;
using Controllers;
using Managers;
using Tags;
using UnityEngine;

namespace Logics
{
    [RequireComponent(typeof(Camera))]
    public class LevelEndCamera : MonoBehaviour
    {
        private Camera _camera;
        private GameObject _player;
        private bool _levelSuccess;
        private CameraFollow _cameraFollow;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _player = FindObjectOfType<PlayerController>().gameObject;
            _cameraFollow = GetComponent<CameraFollow>();
        }

        private void Start()
        {
            _levelSuccess = false;
            GameManager.Instance.LevelSuccess += LevelSuccess;
        }

        private void LevelSuccess()
        {
            _levelSuccess = true;
            _cameraFollow.enabled = false;
            StartCoroutine(DelayedCamera());
        }

        private IEnumerator DelayedCamera()
        {
            yield return null;
        }

        private void LateUpdate()
        {
            if (_levelSuccess)
            {
                var temporaryTransform  = FindObjectOfType<CameraTargetTag>();
                gameObject.transform.LookAt(_player.transform);

                transform.localPosition = Vector3.Lerp(transform.localPosition, temporaryTransform.transform.position,
                    1 * Time.deltaTime);
            }
        }
    }
}