using System;
using Cubes;
using Newtonsoft.Json;
using UnityEngine;

namespace CubeTower
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class TowerEntry
    {
        public int ID;

        public float X;
        public float Y;
        public float Z;


        public TowerEntry(Cube cube)
        {
            ID = cube.ID;
            X = cube.transform.position.x;
            Y = cube.transform.position.y;
            Z = cube.transform.position.z;
        }

        public TowerEntry(int id, Vector3 position)
        {
            ID = id;
            X = position.x;
            Y = position.y;
            Z = position.z;
        }
    }
}