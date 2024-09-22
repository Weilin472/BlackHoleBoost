using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : EnemyBase
{
    protected override void Movement()
    {
        base.Movement();
       transform.rotation= Quaternion.LookRotation(Vector3.forward, _rigid.velocity);
    }
}
