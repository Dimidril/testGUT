using System;
using UnityEngine;

namespace Character.StateMachine.Transitions
{
    public class DirectionTransition : Transition
    {
        [SerializeField] private float _moreThan = 0.1f;
        [SerializeField] private float _lessThan = 1;

        private void Update()
        {
            float directionAbs = Math.Abs(Player.Direction);
            if (directionAbs >= _moreThan && directionAbs <= _lessThan )
            {
                NeedTransition = true;
            }
        }
    }
}