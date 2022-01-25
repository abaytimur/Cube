using System;
using System.Collections;
using Controllers;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logics.Props
{
    public class Obstacles :MonoBehaviour
    {
        private float _slowDuration = 1f;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                UiManager.Instance.HearthSpawner.DecreaseHearths();
                StartCoroutine(SlowCharacter(other.gameObject.GetComponent<PlayerController>()));
            }
        }
        
        private IEnumerator SlowCharacter(PlayerController characterObject)
        {
            characterObject.GetComponent<Animator>().speed = .5f;
            InvokeRepeating(nameof(FovMinusChange),0,.02f);

            characterObject.GetComponent<SlowDownEffect>().PlaySlowDownEffect();
            _slowDuration = Random.Range(1.5f, 2.5f);

            characterObject.movementSpeed = 3f;

            yield return new WaitForSeconds(_slowDuration);
            InvokeRepeating(nameof(FovPlusChange),0,.02f);

            characterObject.GetComponent<SlowDownEffect>().StopSlowDownEffect();
            characterObject.movementSpeed = 5f;
            characterObject.GetComponent<Animator>().speed = 1;

        }

        private void FovMinusChange()
        {
            if (_camera.fieldOfView<= 28f)
            {
                CancelInvoke(nameof(FovMinusChange));
            }

            _camera.fieldOfView -= .3f;
        }
        private void FovPlusChange()
        {
            if (_camera.fieldOfView>= 30f)
            {
                CancelInvoke(nameof(FovPlusChange));
            }

            _camera.fieldOfView += .3f;
        }
    }
}