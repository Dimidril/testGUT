using UnityEngine;

namespace Character.StateMachine
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        protected PlayerMover Player { get; private set; }
        public State TargetState => _targetState;
        public bool NeedTransition { get; protected set; }

        protected virtual void OnEnable()
        {
            NeedTransition = false;
        }

        public void Init(PlayerMover player)
        {
            Player = player;
        }
    }
}