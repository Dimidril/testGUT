using UnityEngine;

namespace Character.StateMachine.States
{
    public class IdleState : State
    {
        private void Update()
        {
            Player.Move(Vector3.zero);
        }
    }
}