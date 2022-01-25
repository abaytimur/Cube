using System;
using System.Collections;
using Managers;
using Unity.Mathematics;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Logics.Props
{

    public class Diamonds: MonoBehaviour
    {
        private string _temporaryTag;
        
        public DiamondShape diamondShape;
        
        private Rigidbody _parentRigidbody;
        private Collider _collider;

        private void Start()
        {
            _parentRigidbody = GetComponentInParent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _collider.enabled = false;
                _parentRigidbody.isKinematic = false;
                _parentRigidbody.useGravity = true;
                _parentRigidbody.AddForce(new Vector3(Random.Range(-20f,20f), 40f, Random.Range(-20f,20f)), ForceMode.Impulse);

                // INCREASE GOLD AMOUNT 
                StartCoroutine(DelayedSpawnDiamondPay());
            }
        }

        private IEnumerator DelayedSpawnDiamondPay()
        {
            yield return new WaitForSeconds(.75f);

            if (diamondShape == DiamondShape.Standard)
            {
                _temporaryTag = Extensions.DiamondPayName;
            }
            else if (diamondShape == DiamondShape.Alternative)
            {
                _temporaryTag = Extensions.DiamondAlternativePayName;
            }
            var temporaryObject =
                ObjectPooler.Instance.SpawnFromPool(_temporaryTag, transform.position, quaternion.identity);

            temporaryObject.transform.parent = null;
            _parentRigidbody.gameObject.SetActive(false);
        }
    }
    public enum DiamondShape
    {
        Standard,
        Alternative
    }
}