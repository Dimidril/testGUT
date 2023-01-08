using System;
using UnityEngine;

namespace Character.StateMachine.Transitions
{
    public class OnJumpPressedTransition : Transition
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Player.OnJumpPressed.AddListener(OnPressed);
        }

        private void OnPressed()
        {
            NeedTransition = true;
        }

        private void OnDisable()
        {
            Player.OnJumpPressed.RemoveListener(OnPressed);
        }
    }
}