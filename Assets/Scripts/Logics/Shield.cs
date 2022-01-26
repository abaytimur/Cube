using Controllers;
using UnityEngine;

namespace Logics
{
    public class Shield : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var temporaryPlayerShield = other.gameObject.GetComponent<PlayerController>().Shield;
                
                temporaryPlayerShield.ActivateShield();
            }
        }
    }
}
