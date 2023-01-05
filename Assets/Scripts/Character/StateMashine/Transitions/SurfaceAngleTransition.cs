using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAngleTransition : Transition
{
    [SerializeField] private float _moreThan = 30f;
    [SerializeField] private float _lessThan = 90f;

    private void Update()
    {
        float angleAbs = Math.Abs(Player.SurfaceAngle);
        if (angleAbs >= _moreThan && angleAbs <= _lessThan)
        {
            NeedTransition = true;
        }
    }
}
