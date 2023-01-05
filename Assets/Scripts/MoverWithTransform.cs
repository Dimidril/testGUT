using UnityEngine;

public class MoverWithTransform : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.1f;

    private Vector3 _cameraOffset;

    private void Awake()
    {
        _cameraOffset = transform.position - _target.position;
    }

    void FixedUpdate()
    {
        Vector3 pos = new Vector3(_target.position.x, _target.position.y + _cameraOffset.y, _target.position.z + _cameraOffset.z);
        transform.position = Vector3.Lerp(transform.position, pos, _smoothSpeed);
    }

    public void SetOffSet(float offset)
    {
        _cameraOffset *= offset;
    }
}