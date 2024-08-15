using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivativeTower : TowerController
{
    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        _target = UpdateTarget();

        if (_target != null)
        {
            RotateTowardsTarget();
        }
    }
}
