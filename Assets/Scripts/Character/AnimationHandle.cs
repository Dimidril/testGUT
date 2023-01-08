using System;
using Character.StateMachine;
using Character.StateMachine.States;
using Spine.Unity;
using UnityEngine;

namespace Character
{
    public class AnimationHandle : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private CharacterStateMachine _characterStateMachine;

        private void Start()
        {
            SetAnimation("portal", false);
            _skeletonAnimation.AnimationState.GetCurrent(0).Complete += entry => SetAnimation( "idle", false);

            _characterStateMachine.OnStateChange.AddListener(OnCharacterStateChange);

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
                SetAnimation("idle", true);
            else if(stateType == typeof(MoveState))
                SetAnimation("run", true);
            else if(stateType == typeof(SlidingState))
                SetAnimation("hoverboard", true);
            else if (stateType == typeof(JumpState))
                SetAnimation("jump", false);
            
        
        }

        private void SetAnimation(string name, bool isLoop)
        {
            _skeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
        }
    }
}