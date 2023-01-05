using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    public PlayerMover Player { get; private set; }

    public virtual void Enter(PlayerMover player)
    {
        if (!enabled)
        {
            Player = player;
            enabled = true;

            _transitions.ForEach(t => {
                t.enabled = true;
                t.Init(Player);
            });
        }
    }

    public virtual void Exit()
    {
        if (enabled)
        {
            enabled = false;

            _transitions.ForEach(t => t.enabled = false);
        }
    }

    public State GetNextState()
    {
        foreach(var transition in _transitions) 
        {
            if (transition.NeedTransition)
                return transition.TargetState;
        }

        return null;
    }
}