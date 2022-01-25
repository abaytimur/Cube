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
                _parentRigidbody.AddForce(new Vector3(Random.Range(-2f,2f), 4f, Random.Range(-2f,2f)), ForceMode.Impulse);

                // INCREASE GOLD AMOUNT 
                StartCoroutine(DelayedSpawnDiamondPay());
            }
        }

        private IEnumerator DelayedSpawnDiamondPay()
        {
            yield return new WaitForSeconds(.4f);

            var temporaryObject =
                ObjectPooler.Instance.SpawnFromPool(Extensions.DiamondPayName, transform.position, quaternion.identity);

            temporaryObject.transform.parent = null;
            _parentRigidbody.gameObject.SetActive(false);
        }
    }
}