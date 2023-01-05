using System;
using Spine.Unity;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private CharacterStateMashine _characterStateMashine;

    private void Start()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "portal", false);
        _skeletonAnimation.AnimationState.GetCurrent(0).Complete += entry => _skeletonAnimation.AnimationState.SetAnimation(0, "idle", false);

        _characterStateMashine.OnStateChange.AddListener(OnCharacterStateChange);

        _playerMover.OnDirectionChanged.AddListener(SetFlip);
    }
    
    public void SetFlip (float horizontal) {
        if (horizontal != 0) {
            _skeletonAnimation.Skeleton.ScaleX = horizontal;
        }
    }

    private void OnCharacterStateChange(State newState)
    {
        Type stateType = newState.GetType();

        if (stateType == typeof(IdleState))
        {
            SetAnimation("idle", true);
        }
        else if(stateType == typeof(MoveState))
        {
            SetAnimation("run", true);
        }
        else if(stateType == typeof(SlidingState))
            SetAnimation("hoverboard", true);
        
    }

    private void SetAnimation(string name, bool isLoop)
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }
}