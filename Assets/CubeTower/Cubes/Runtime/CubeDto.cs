using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CubeTower.Cubes.Runtime
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class CubeDto
    {
        [JsonProperty("id")][SerializeField] private int _id;
        [JsonProperty("sprite_name")][SerializeField] private string _spriteName;

        public string SpriteName => _spriteName;
        public int ID => _id;
    }
}