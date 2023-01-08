using System;
using System.Collections;
using UnityEngine;

namespace Character.StateMachine.Transitions
{
    public class OnGroundedTransition : Transition
    {
        [SerializeField] private bool _isTransitOnIsGrounded;
        [SerializeField] private float _delay;

        private bool _isDelayComplete;

        protected override void OnEnable()
        {
            base.OnEnable();
            _isDelayComplete = false;
            StartCoroutine(Delaying());
        }

        private void Update()
        {
            if (_isDelayComplete && Player.IsGrounded() == _isTransitOnIsGrounded)
            {
                NeedTransition = true;
            }
        }

        private IEnumerator Delaying()
        {
            yield return new WaitForSeconds(_delay);
            _isDelayComplete = true;
        }
    }
}