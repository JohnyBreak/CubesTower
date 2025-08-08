using System;
using Cubes;
using Cubes.Config;
using Cubes.UI;
using Pool;
using UnityEngine;

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
    private readonly CubePool _pool;
    
    private readonly ConfigContainer _dtos;

    public CubeFactory(
        IconsProvider iconsProvider, 
        AssetProvider assetProvider,
        CubePool pool,
        ConfigContainer dtos)
    {
        _iconsProvider = iconsProvider;
        _pool = pool;
        var prefab = assetProvider.LoadAssetSync<GameObject>(CubePrefabKey).GetComponent<Cube>();
        _pool.SetPrefab(prefab);
        _dtos = dtos;
    }

    public Cube GetCube(int id)
    {
        var dto = _dtos.CubesDtos.Dtos[id];
        var cube = _pool.GetPooledObject(true);
        
        var sprite = _iconsProvider.GetSprite(_dtos.CubesDtos.SpriteSheetName, dto.SpriteName);
        cube.SetSprite(sprite);
        cube.SetId(id);
        return cube;
    }
}
