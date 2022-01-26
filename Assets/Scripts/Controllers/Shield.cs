using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class Shield : MonoBehaviour
    {
        private bool _shielded;
        public bool Shielded => _shielded;

        public void ActivateShield()
        {
            StartCoroutine(DelayedShield());
        }

        private IEnumerator DelayedShield()
        {
            _shielded = true;
            yield return new WaitForSeconds(5f);
            _shielded = false;
        }
    }
}