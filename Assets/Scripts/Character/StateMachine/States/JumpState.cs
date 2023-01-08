using System;
using UnityEngine;

namespace Character.StateMachine.States
{
    public class JumpState : State
    {
        [SerializeField] private float _force;
        [SerializeField] private float _moveSpeedInJump;
        
        public override void Enter(PlayerMover player)
        {
            base.Enter(player);
            Player.Jump(_force);
        }

        private void Update()
        {
            Player.Move(Vector2.right * Player.Direction * _moveSpeedInJump);
        }
    }
}