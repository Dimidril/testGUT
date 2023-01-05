using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CharacterStateMashine _characterStateMashine;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        _characterStateMashine.OnStateChange.AddListener(state => {
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