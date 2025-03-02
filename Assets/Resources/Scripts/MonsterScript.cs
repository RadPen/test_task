using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterScript : HealthControlling
{

    public override void Die()
    {
        DropItem();
        base.Die();
    }

    void DropItem()
    {
        spawner.CreateCreature(new Creature(new ParametersData
        {
            type = Type.TypeCreature.Item,
            position = transform.position
        }));
    }
}
