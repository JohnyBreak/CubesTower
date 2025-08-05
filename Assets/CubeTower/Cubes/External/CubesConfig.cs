using System;
using UnityEngine;

namespace Cubes.Config
{
    [CreateAssetMenu(fileName = "CubesConfig", menuName = "Cubes/CubesConfig")]
    public class CubesConfig : ScriptableObject
    {
        public const string SpriteSheetName = "CubesSpriteSheet";
        public CubeDto[] Dtos;
    }

    [Serializable]
    public class CubeDto
    {
        [SerializeField] private int _id;
        [SerializeField] private string _spriteName;

        public string SpriteName => _spriteName;
        public int ID => _id;
    }
}

