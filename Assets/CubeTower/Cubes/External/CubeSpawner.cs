using System;
using Cubes;
using Cubes.Config;
using UnityEngine;
using Object = UnityEngine.Object;

public class CubeSpawner : IDisposable
{
    private readonly Dragger _dragger;
    private readonly ScrollView _scrollView;
    private readonly ScreenWorldUtility _utility;
    private readonly CubeFactory _factory;
    
    public CubeSpawner(
        ScrollView scrollView,
        ScreenWorldUtility utility, 
        CubeFactory factory,
        Dragger dragger)
    {
        scrollView.OnCubeSelectEvent += Spawn;
        _scrollView = scrollView;
        _utility = utility;
        _factory = factory;
        _dragger = dragger;
    }

    public void Spawn(int id)
    {
        var spawnPos = _utility.GetMouseWorldPosition();
        spawnPos.z = 0;

        var cube = _factory.GetCube(id);
        cube.transform.position = spawnPos;
        
        _dragger.SetTarget(cube);
    }

    public void Dispose()
    {
        _scrollView.OnCubeSelectEvent -= Spawn;
    }
}

public class CubeFactory
{
    private const string CubePrefabKey = "Cube";
    
    private readonly IconsProvider _iconsProvider;
    private readonly Cube _prefab;
    
    private readonly CubeDto[] _dtos;

    public CubeFactory(IconsProvider iconsProvider, AssetProvider assetProvider, CubeDto[] dtos)
    {
        _iconsProvider = iconsProvider;
        _prefab = assetProvider.LoadAssetSync<GameObject>(CubePrefabKey).GetComponent<Cube>();
        _dtos = dtos;
    }

    public Cube GetCube(int id)
    {
        var dto = _dtos[id];
        var cube = Object.Instantiate(_prefab);
        var sprite = _iconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName);
        cube.SetSprite(sprite);
        return cube;
    }
}
