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
    
    private PlayerInput PlayerInput;
    private bool _isCanFlip = true;

    public float Direction { get; private set; }
    public float SurfaceAngle { get; private set; }

    
    public UnityEvent<float> OnDirectionChanged;
    

    private void OnValidate()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();
    }

    private void Update()
    {
        float newDirection = PlayerInput.Move.Move.ReadValue<float>();

        if (newDirection != Direction)
        {
            Direction = newDirection;
            if(_isCanFlip)
                OnDirectionChanged?.Invoke(Direction);
        }

        SurfaceAngle = SlidingAngle();
    }


    private float SlidingAngle()
    {
        var ray = new Ray(transform.position, Vector3.down);
        float slopeRotation = 0;

        if (Physics.Raycast(ray, out RaycastHit hit, _characterController.height))
        {
            Debug.DrawLine(transform.position, hit.normal);
            slopeRotation = -Vector3.SignedAngle(transform.up, hit.normal, Vector3.forward);
        }
        
        return slopeRotation;
    }

    public void Move(Vector3 velosity)
    {
        velosity.y = -9.8f;
        _characterController.Move(velosity * Time.deltaTime);
    }

    internal void SetCanFlip(bool value)
    {
        _isCanFlip = value;
    }
}