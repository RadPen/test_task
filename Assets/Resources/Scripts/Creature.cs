using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public readonly ParametersData parameterCreature;
    public readonly GameObject ñreature;

    public Creature(ParametersData parameter)
    {
        parameterCreature = parameter;
        ñreature = TakeAnObject(parameterCreature);
    }

    public GameObject Create(Transform paarentTransform)
    {
        var obj = Object.Instantiate(ñreature, parameterCreature.position, Quaternion.identity, paarentTransform);
        obj.GetComponent<CreatureType>().yourPrefab = ñreature;
        return obj;
    }

    private GameObject TakeAnObject (ParametersData parameter)
    {
        if (parameter.prefab == null)
            parameter.prefab = FindCreatureOfType(parameter.name, parameter.type);
        return parameter.prefab;
    }

    public GameObject FindCreatureOfType(string ñreatureName, Type.TypeCreature type)
    {
        switch (type)
        {
            case Type.TypeCreature.Monster:
                return LoadPrefab(PathResources.Enemy, ñreatureName);
            case Type.TypeCreature.Item:
                return LoadPrefab(PathResources.Item, ñreatureName);
            default:
                return null;
        }
    }

    private GameObject LoadPrefab(string folderName, string ñreatureName = "")
    {
        GameObject creat = Resources.Load<GameObject>(folderName + ñreatureName);
        if (creat == null)
        {
            var prefabs = Resources.LoadAll<GameObject>(folderName);
            creat = prefabs[Random.Range(0, prefabs.Length)];
        }
        return creat;
    }
}
