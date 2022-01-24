using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SwerveController : MonoBehaviour
    {
        private static Vector3 _moveForce;
        private static bool _onTheEdge = false;

        private bool _canMove;
        private Rigidbody _rigidbody;

        private Vector3 _lastMousePos;
        private Vector3 _lastTransform;

        [SerializeField] private float sensitivity = 0.16f, clampDelta = 42f;
        [SerializeField] private float turnSpeed = 15;
        [SerializeField] private float maxX = 3.7f;


        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            GameManager.Instance.LevelStart += LevelStarted;
            GameManager.Instance.LevelSuccess += LevelSuccess;
            GameManager.Instance.LevelFail += LevelFail;
        }

        private void OnDestroy()
        {
            GameManager.Instance.LevelStart -= LevelStarted;
            GameManager.Instance.LevelSuccess -= LevelSuccess;
            GameManager.Instance.LevelFail -= LevelFail;
        }

        private void LevelStarted()
        {
            _rigidbody.isKinematic = false;
            _canMove = true;
        }

        private void LevelFail()
        {
            _canMove = false;
        }

        private void LevelSuccess()
        {
            _canMove = false;
        }

        private void Update()
        {
            TurnThePlayer();
        }

        private void FixedUpdate()
        {
            if (!_canMove)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePos = Input.mousePosition;
                _lastTransform = transform.position;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 direction = _lastMousePos - Input.mousePosition;
                _lastMousePos = Input.mousePosition;
                _lastTransform = transform.position;
                direction = new Vector3(direction.x, 0, 0);
                _moveForce = Vector3.ClampMagnitude(direction, clampDelta);

                if (transform.position.x >= maxX)
                {
                    if (_moveForce.x < 0)
                    {
                        //right
                        _onTheEdge = true;
                    }
                    else
                    {
                        _onTheEdge = false;
                    }
                }
                else if (transform.position.x <= -maxX)
                {
                    if (_moveForce.x > 0)
                    {
                        //left
                        _onTheEdge = true;
                    }
                    else
                    {
                        _onTheEdge = false;
                    }
                }

                if (!_onTheEdge)
                {
                    _rigidbody.AddForce((-_moveForce * sensitivity - _rigidbody.velocity / 5f),
                        ForceMode.VelocityChange);
                }
            }
        }

        void TurnThePlayer()
        {
            if (!_onTheEdge)
            {
                if (transform.position.x > _lastTransform.x && _canMove)
                {
                    //right
                    print("Right");
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 18, 0),
                        Time.deltaTime * turnSpeed);
                }
                else if (transform.position.x < _lastTransform.x && _canMove)
                {
                    //left
                    print("Left");
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -18, 0),
                        Time.deltaTime * turnSpeed);
                }
                else if (transform.position.x == _lastTransform.x)
                {
                    //midle
                    print("Midle");
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0),
                        Time.deltaTime * turnSpeed);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0),
                        Time.deltaTime * turnSpeed);
                }
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}