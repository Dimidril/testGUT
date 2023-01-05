using UnityEngine;

public class IdleState : State
{
    private void Update()
    {
        Player.Move(Vector3.zero);
    }
}