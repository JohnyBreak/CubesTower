using System;
using CubeTower.Common.Data;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class TowerData : IData
{
    [SerializeField] private int _count;

    public TowerData()
    {
        
    }

    public int Count
    {
        get => _count;
        set => _count = value;
    }

    public string Name()
    {
        return nameof(TowerData); 
    }
}
