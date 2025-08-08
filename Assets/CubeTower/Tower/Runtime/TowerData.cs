using System;
using System.Collections.Generic;
using CubeTower.Common.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace CubeTower
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class TowerData : IData
    {
        [SerializeField] private List<TowerEntry> _entries;

        public List<TowerEntry> Entries
        {
            get => _entries;
            set => _entries = value;
        }

        public string Name()
        {
            return nameof(TowerData);
        }

        public void WhenCreateNewData()
        {
            _entries = new();
        }
    
        public void BeforeSerialize()
        {
        }
    }
}
