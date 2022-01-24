using System;
using Controllers;
using UnityEngine;

namespace Logics
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _target;

        [SerializeField] private float smoothSpeed = .125f;
        public float smoothTime = 0.3f;

        [SerializeField] private Vector3 offset;
        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            _target = FindObjectOfType<PlayerController>().transform;
        }

        void LateUpdate()
        {
            Vector3 desiredPosition = _target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity,
                smoothTime * Time.deltaTime);

            //     transform.LookAt(_target);
        }
    }
}