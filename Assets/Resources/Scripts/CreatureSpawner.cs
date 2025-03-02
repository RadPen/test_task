using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject CreateCreature(Creature creat)
    {
        return creat.Create(this.transform);
    }

    public void CreateMultipleCreature(int count, Rect field, Creature creat)
    {
        for (int i = 0; i < count; i++)
        {
            creat.parameterCreature.position = GetRandomPosition(field);
            CreateCreature(creat);
        }
    }

    private Vector2 GetRandomPosition(Rect field)
    {
        var randomX = Random.Range(field.x, field.width);
        var randomY = Random.Range(field.y, field.height);

        return new Vector2(randomX, randomY);
    }
}