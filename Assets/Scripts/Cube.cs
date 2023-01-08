using Character.StateMachine;
using Character.StateMachine.States;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine _characterStateMachine;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _characterStateMachine.OnStateChange.AddListener(state => {
            if(state.GetType() == typeof(SlidingState))
            {
                _meshRenderer.material.color = Color.red;
            }
            else
            {
                _meshRenderer.material.color = Color.white;
            }

        });
    }
}