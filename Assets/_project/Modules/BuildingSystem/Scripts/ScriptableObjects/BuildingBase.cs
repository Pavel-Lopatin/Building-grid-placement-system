using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingBase", menuName = "Scriptable Objects/BuildingBase")]
public class BuildingBase : ScriptableObject
{
    public BuildingData[] buildings;
}

[Serializable]
public class BuildingData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}