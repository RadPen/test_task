using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public readonly ParametersData parameterCreature;
    public readonly GameObject �reature;

    public Creature(ParametersData parameter)
    {
        parameterCreature = parameter;
        �reature = TakeAnObject(parameterCreature);
    }

    public GameObject Create(Transform paarentTransform)
    {
        var obj = Object.Instantiate(�reature, parameterCreature.position, Quaternion.identity, paarentTransform);
        obj.GetComponent<CreatureType>().yourPrefab = �reature;
        return obj;
    }

    private GameObject TakeAnObject (ParametersData parameter)
    {
        if (parameter.prefab == null)
            parameter.prefab = FindCreatureOfType(parameter.name, parameter.type);
        return parameter.prefab;
    }

    public GameObject FindCreatureOfType(string �reatureName, Type.TypeCreature type)
    {
        switch (type)
        {
            case Type.TypeCreature.Monster:
                return LoadPrefab(PathResources.Enemy, �reatureName);
            case Type.TypeCreature.Item:
                return LoadPrefab(PathResources.Item, �reatureName);
            default:
                return null;
        }
    }

    private GameObject LoadPrefab(string folderName, string �reatureName = "")
    {
        GameObject creat = Resources.Load<GameObject>(folderName + �reatureName);
        if (creat == null)
        {
            var prefabs = Resources.LoadAll<GameObject>(folderName);
            creat = prefabs[Random.Range(0, prefabs.Length)];
        }
        return creat;
    }
}
