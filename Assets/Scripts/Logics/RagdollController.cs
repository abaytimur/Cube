using Controllers;
using UnityEngine;

namespace Logics
{
    public class RagdollController : MonoBehaviour
    {
        private void Start()
        {
            SetRigidBodyState(true);
            SetColliderState(false);
        }

        private void SetRigidBodyState(bool state)
        {
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (var item in rigidbodies)
            {
                item.isKinematic = state;
            }
        }

        private void SetColliderState(bool state)
        {
            Collider[] rigidbodies = GetComponentsInChildren<Collider>();

            foreach (var item in rigidbodies)
            {
                item.enabled = state;
            }
        }

        public void Die()
        {
            PlayerController.Instance.Animator.enabled = false;
            SetRigidBodyState(false);
            SetColliderState(true);
        }
    }
}
