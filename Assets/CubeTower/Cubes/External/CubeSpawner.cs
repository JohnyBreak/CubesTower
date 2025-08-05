using Cubes;
using Cubes.Config;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubesConfig _config;
    [SerializeField] private Draggable _prefab;

    [SerializeField] private Transform _spawnPosition;

    public void Spawn(int id)
    {
        var dto = _config.Dtos[id];

        var cube = Instantiate(_prefab, _spawnPosition.position, Quaternion.identity);
        
        var sprite = IconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName);
        cube.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
