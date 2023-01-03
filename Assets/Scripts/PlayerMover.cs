using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed;

    private CharacterState _state = CharacterState.Idle;
    private CharacterState _previousState;
    private PlayerInput _playerInput;
    private float _direction;
    private Vector2 _velocity;

    public UnityEvent<CharacterState> OnStateChange;
    
    public UnityEvent OnStop;
    public UnityEvent OnMove;
    public UnityEvent<float> OnDirectionChanged;

    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Move.Move.performed += cbc =>
        {
            OnMove?.Invoke();
        };
        _playerInput.Move.Move.canceled += cbc =>
        {
            OnStop?.Invoke();
        };
    }

    private void Update()
    {
        _previousState = _state;
        float slidingAngle = SlidingAngle();
        float newDirection = _playerInput.Move.Move.ReadValue<float>();
        
        if (slidingAngle > 30f)
            _velocity = new Vector2(_moveSpeed, -9.8f);
        else
        {
            if(!_characterController.isGrounded)
                _velocity.y = -9.8f;
        
            _velocity.x = _direction * _moveSpeed;
        }
        
        if (newDirection != _direction)
        {
            _direction = newDirection;
            OnDirectionChanged.Invoke(_direction);
        }
        
        _characterController.Move(_velocity * Time.deltaTime);
        
        
        
        if (slidingAngle > 30f && _state != CharacterState.Sliding)
        {
            _state = CharacterState.Sliding;
            OnStateChange?.Invoke(_state);
        }
        else if(Math.Abs(_velocity.x) > 0 && _state != CharacterState.Run)
        {
            _state = CharacterState.Run;
            OnStateChange?.Invoke(_state);
        }
        else if(_state != CharacterState.Idle)
        {
            _state = CharacterState.Idle;
            OnStateChange?.Invoke(_state);
        }
    }

    private float SlidingAngle()
    {
        var ray = new Ray(transform.position, Vector3.down);
        float slopeRotation = 0;

        if (Physics.Raycast(ray, out RaycastHit hit, _characterController.height))
        {
            slopeRotation = Vector3.SignedAngle(transform.up, hit.normal, Vector3.up);

            Debug.Log(slopeRotation);
        }
        
        return slopeRotation;
    }
}


public enum CharacterState
{
    Idle,
    Run,
    Sliding
}
