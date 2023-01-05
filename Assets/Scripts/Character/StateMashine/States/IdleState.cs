using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    private void Update()
    {
        Player.Move(Vector3.zero);
    }
}
