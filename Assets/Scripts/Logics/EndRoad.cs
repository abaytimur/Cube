using System.Collections;
using Controllers;
using Managers;
using UnityEngine;

namespace Logics
{
    public class EndRoad : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] confetties;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(DelayedEndGame());
            }
        }

        private IEnumerator DelayedEndGame()
        {
            GameManager.Instance.OnLevelSuccess();

            StartCoroutine(ConfettiShower());
            
            PlayerController.Instance._canMove = false;
            PlayerController.Instance.SwerveController.canMove = false;
            PlayerController.Instance.Animator.SetBool("Win", true);
            
                yield return new WaitForSeconds(1f);

            PlayerController.Instance.movementSpeed = 0;
        }

        private IEnumerator ConfettiShower()
        {
            while (true)
            {
                for (int i = 0; i < confetties.Length; i++)
                {
                    yield return new WaitForSeconds(.3f);

                    confetties[i].Play(true);
                }
            }
        }
    }
}
