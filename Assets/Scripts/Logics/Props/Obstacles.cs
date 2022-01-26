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

        private float _initialCharacterMovementSpeed;
        private float _halvedCharacterMovementSpeed;

      
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _initialCharacterMovementSpeed = PlayerController.Instance.movementSpeed;
            _halvedCharacterMovementSpeed = PlayerController.Instance.movementSpeed*2 / 3;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var temporaryPlayerShield = other.GetComponent<PlayerController>().Shield;
                if (!temporaryPlayerShield.Shielded)
                {
                    UiManager.Instance.HearthSpawner.DecreaseHearths();
                    StartCoroutine(SlowCharacter(other.gameObject.GetComponent<PlayerController>()));
                }
            }
        }
        
        private IEnumerator SlowCharacter(PlayerController characterObject)
        {
            characterObject.GetComponent<Animator>().speed = .5f;
            InvokeRepeating(nameof(FovMinusChange),0,.02f);

            characterObject.GetComponent<SlowDownEffect>().PlaySlowDownEffect();
            _slowDuration = Random.Range(1f, 2f);

            characterObject.movementSpeed = _halvedCharacterMovementSpeed;

            yield return new WaitForSeconds(_slowDuration);
            InvokeRepeating(nameof(FovPlusChange),0,.02f);

            characterObject.GetComponent<SlowDownEffect>().StopSlowDownEffect();
            characterObject.movementSpeed = _initialCharacterMovementSpeed;
            characterObject.GetComponent<Animator>().speed = 1;

        }

        private void FovMinusChange()
        {
            if (_camera.fieldOfView<= 22.5f)
            {
                CancelInvoke(nameof(FovMinusChange));
            }

            _camera.fieldOfView -= .3f;
        }
        private void FovPlusChange()
        {
            if (_camera.fieldOfView>= 25f)
            {
                CancelInvoke(nameof(FovPlusChange));
            }

            _camera.fieldOfView += .3f;
        }
    }
}