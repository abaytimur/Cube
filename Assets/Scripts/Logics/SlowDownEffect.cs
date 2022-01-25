using System.Collections;
using UnityEngine;

namespace Logics
{
    public class SlowDownEffect : MonoBehaviour
    {
        [SerializeField] private Material shaderSlowDownEffectMaterial;
        [SerializeField] private SkinnedMeshRenderer[] shaderMaterialSocket;

        private Material _initialMaterial;

        private bool _slowDown;

        
        private void Awake()
        {
            for (int i = 0; i < shaderMaterialSocket.Length; i++)
            {
                _initialMaterial = shaderMaterialSocket[i].material;
            }
        }

        public void PlaySlowDownEffect()
        {
            _slowDown = true;
            StartCoroutine(DelayedPlay());
        }

        public void StopSlowDownEffect()
        {
            _slowDown = false;
            StopAllCoroutines();
            for (int i = 0; i < shaderMaterialSocket.Length; i++)
            {
                shaderMaterialSocket[i].material = _initialMaterial;
            }
       
        }

        private IEnumerator DelayedPlay()
        {
            while (_slowDown)
            {
                for (int i = 0; i < shaderMaterialSocket.Length; i++)
                {
                    shaderMaterialSocket[i].material = shaderSlowDownEffectMaterial;
                }
         
                yield return new WaitForSeconds(.15f);
                for (int i = 0; i < shaderMaterialSocket.Length; i++)
                {
                    shaderMaterialSocket[i].material = _initialMaterial;
                }
                yield return new WaitForSeconds(.15f);
            }
        }
    }
}
