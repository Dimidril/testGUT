using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private PlayerMover _playerMover;

    private void Start()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "portal", false);
        _skeletonAnimation.AnimationState.GetCurrent(0).Complete += entry => _skeletonAnimation.AnimationState.SetAnimation(0, "idle", false);
        
        _playerMover.OnStateChange.AddListener(OnCharacterStateChange);
        
        /*_playerMover.OnMove.AddListener(() =>
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "run", true);
        });
        _playerMover.OnStop.AddListener(() =>
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        });*/
        _playerMover.OnDirectionChanged.AddListener(SetFlip);
    }
    
    public void SetFlip (float horizontal) {
        if (horizontal != 0) {
            _skeletonAnimation.Skeleton.ScaleX = horizontal;
        }
    }

    private void OnCharacterStateChange(CharacterState newState)
    {
        switch (newState)
        {
            case CharacterState.Idle:
                SetAnimation("idle", true);
                break;
            case CharacterState.Run: 
                SetAnimation("run", true);
                break;
            case CharacterState.Sliding:
                SetAnimation("hoverboard", true);
                break;
        }
    }

    private void SetAnimation(string name, bool isLoop)
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }
}
