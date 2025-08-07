using System;
using CubeTower.Cubes.Runtime;
using Newtonsoft.Json;
using UnityEngine;

namespace Cubes.Config
{
    [CreateAssetMenu(fileName = "CubesConfig", menuName = "Cubes/CubesConfig")]
    public class CubesConfig : ScriptableObject
    {
        public const string SpriteSheetName = "CubesSpriteSheet";
        public CubeDto[] Dtos;
    }
}

