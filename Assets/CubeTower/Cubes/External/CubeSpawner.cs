using Cubes;
using Cubes.Config;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private CubesConfig _config;
    [SerializeField] private Cube _prefab;
    [SerializeField] private Dragger _dragger;

    public void Spawn(int id)
    {
        var dto = _config.Dtos[id];
        var spawnPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = 0;
        var cube = Instantiate(_prefab, spawnPos, Quaternion.identity);
        
        var sprite = IconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName);
        cube.GetComponent<SpriteRenderer>().sprite = sprite;
        
        _dragger.SetTarget(cube);
    }
}
