using UnityEngine;
using UnityEngine.Events;

public class CharacterStateMashine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private PlayerMover _player;

    private State _currentState;

    public UnityEvent<State> OnStateChange;

    private void Start()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);

        //Debug.Log(_currentState.name);
    }

    private void Reset(State startState)
    {
        _currentState = startState;
        OnStateChange?.Invoke(_currentState);

        if (_currentState != null)
            _currentState.Enter(_player);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;
        OnStateChange?.Invoke(_currentState);
        Debug.Log(_currentState.name);


        if (_currentState != null)
            _currentState.Enter(_player);
    }
}