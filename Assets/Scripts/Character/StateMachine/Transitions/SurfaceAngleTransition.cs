using System;
using UnityEngine;

namespace Character.StateMachine.Transitions
{
    public class SurfaceAngleTransition : Transition
    {
        [SerializeField] private float _moreThan = 30f;
        [SerializeField] private float _lessThan = 90f;

        private void Update()
        {
            float angleAbs = Math.Abs(Player.SlidingAngle());
            if (angleAbs >= _moreThan && angleAbs <= _lessThan)
            {
                NeedTransition = true;
            }
        }
    }
}