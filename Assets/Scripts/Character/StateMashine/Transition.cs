
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected PlayerMover Player { get; private set; }
    public State TargetState => _targetState;
    public bool NeedTransition { get; protected set; }

    private void OnEnable()
    {
        NeedTransition = false;
    }

    public void Init(PlayerMover player)
    {
        Player = player;
    }
}
