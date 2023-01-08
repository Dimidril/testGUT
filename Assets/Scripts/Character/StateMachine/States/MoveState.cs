using UnityEngine;

namespace Character.StateMachine.States
{
    public class MoveState : State
    {
        [SerializeField] private float _speed = 4;

        private void Update()
        {
            Player.Move(Vector2.right * Player.Direction * _speed);
        }
    }
}