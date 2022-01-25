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

        [HideInInspector] public bool canMove;
        private Rigidbody _rigidbody;

        private Vector3 _lastMousePos;
        private Vector3 _lastTransform;

        [SerializeField] private float sensitivity = 0.16f, clampDelta = 42f;
        [SerializeField] private float turnSpeed = 15;
        [SerializeField] private float maxX = 3.7f;
        private Vector3 _direction;

        [SerializeField] private GameObject childRotationObject;

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
            canMove = true;
        }

        private void LevelFail()
        {
            canMove = false;
            _rigidbody.isKinematic = true;
        }

        private void LevelSuccess()
        {
            canMove = false;
            _rigidbody.isKinematic = true;
        }

        private void FixedUpdate()
        {
            if (!canMove)
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
                TurnThePlayer();

                _direction = _lastMousePos - Input.mousePosition;
                _lastMousePos = Input.mousePosition;
                _lastTransform = transform.position;
                _direction = new Vector3(_direction.x, 0, 0);
                _moveForce = Vector3.ClampMagnitude(_direction, clampDelta);

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
                else
                {
                    childRotationObject.transform.rotation = Quaternion.Lerp(childRotationObject.transform.rotation,
                        Quaternion.Euler(0, 0, 0),
                        Time.deltaTime * turnSpeed);
                }

                if (!_onTheEdge)
                {
                    _rigidbody.AddForce((-_moveForce * sensitivity - _rigidbody.velocity / 5f),
                        ForceMode.VelocityChange);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _rigidbody.velocity = Vector3.zero;
               
            }
            

        }

        void TurnThePlayer()
        {
            if (!_onTheEdge)
            {
                if (transform.position.x > _lastTransform.x && canMove)
                {
                    //right
                    print("Right");
                    childRotationObject.transform.rotation =Quaternion.Lerp(childRotationObject.transform.rotation,
                        Quaternion.Euler(0, 18, 0),
                        Time.deltaTime * turnSpeed);
                }
                else if (transform.position.x < _lastTransform.x && canMove)
                {
                    //left
                    print("Left");
                    childRotationObject.transform.rotation = Quaternion.Lerp(childRotationObject.transform.rotation,
                        Quaternion.Euler(0, -18, 0),
                        Time.deltaTime * turnSpeed);
                }
                else if (transform.position.x == _lastTransform.x)
                {
                    //midle
                    print("Midle");
                    childRotationObject.transform.rotation = Quaternion.Lerp(childRotationObject.transform.rotation,
                        Quaternion.Euler(0, 0, 0),
                        Time.deltaTime * turnSpeed);
                }
                else
                {
                    childRotationObject.transform.rotation = Quaternion.Lerp(childRotationObject.transform.rotation,
                        Quaternion.Euler(0, 0, 0),
                        Time.deltaTime * turnSpeed);
                }
            }
            else
            {
                childRotationObject.transform.rotation = Quaternion.Lerp(childRotationObject.transform.rotation,
                    Quaternion.Euler(0, 0, 0),
                    Time.deltaTime * turnSpeed);
                
            }
        }
    }
}