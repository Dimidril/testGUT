using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rayToGroundLenght = 0.33f;

        private PlayerInput _playerInput;
        private bool _isCanFlip = true;

        public float Direction { get; private set; }

        public UnityEvent<float> OnDirectionChanged;
        public UnityEvent OnJumpPressed;

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, Vector3.down * _rayToGroundLenght, Color.red);
        }

        private void OnValidate()
        {
            _collider = GetComponent<CapsuleCollider>();
        }

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();

            _playerInput.Move.Jump.performed += context => OnJumpPressed?.Invoke();
        }

        private void Update()
        {
            float newDirection = _playerInput.Move.Move.ReadValue<float>();

            if (newDirection != Direction)
            {
                Direction = newDirection;
                if(_isCanFlip)
                    OnDirectionChanged?.Invoke(Direction);
            }
        }

        public float SlidingAngle()
        {
            var ray = new Ray(transform.position, Vector3.down);
            float slopeRotation = 0;

            if (Physics.Raycast(ray, out RaycastHit hit, _rayToGroundLenght))
            {
                slopeRotation = -Vector3.SignedAngle(transform.up, hit.normal, Vector3.forward);
            }
        
            return slopeRotation;
        }

        public bool IsGrounded()
        {
            var ray = new Ray(transform.position, Vector3.down);

            return Physics.Raycast(ray, out RaycastHit hit, _rayToGroundLenght);
        }

        public void Move(Vector3 velocity)
        {
            _rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
        }

        public void Jump(float force)
        {
            _rigidbody.AddForce(Vector2.up * force);
        }

        internal void SetCanFlip(bool value)
        {
            _isCanFlip = value;
        }
    }
}