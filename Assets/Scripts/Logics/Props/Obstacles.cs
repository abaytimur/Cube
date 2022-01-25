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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("obstacle collided");
                
                UiManager.Instance.HearthSpawner.DecreaseHearths();
                StartCoroutine(SlowCharacter(other.gameObject.GetComponent<PlayerController>()));
            }
        }
        
        private IEnumerator SlowCharacter(PlayerController characterObject)
        {
            characterObject.GetComponent<Animator>().speed = .5f;

            characterObject.GetComponent<SlowDownEffect>().PlaySlowDownEffect();
            _slowDuration = Random.Range(1.5f, 2.5f);

            characterObject.movementSpeed = 3f;

            yield return new WaitForSeconds(_slowDuration);

            characterObject.GetComponent<SlowDownEffect>().StopSlowDownEffect();
            characterObject.movementSpeed = 5f;
            characterObject.GetComponent<Animator>().speed = 1;

        }
    }
}