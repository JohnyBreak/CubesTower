using Cubes;
using CubeTower;
using CubeTower.Common.Data;
using UnityEngine;

public class TowerLoader
{
    private readonly Tower _tower;
    private readonly CubeFactory _factory;
    private TowerData _data;
    
    public TowerLoader(
        Tower tower,
        CubeFactory factory,
        IDataManager dataManager)
    {
        _tower = tower;
        _factory = factory;
        _data = dataManager.GetData(nameof(TowerData)) as TowerData;
        if (_data == null)
        {
            Debug.LogError("TowerData == null");
        }

        Load();
    }

    private void Load()
    {
        foreach (var entry in _data.Entries)
        {
            var cube = _factory.GetCube(entry.ID);
            cube.transform.position = new Vector3(entry.X, entry.Y, entry.Z);
            _tower.AddSilent(cube);
        }
    }
    
    public void Save()
    {
        _data.Entries.Clear();
        foreach (var cube in _tower.Nodes)
        {
            _data.Entries.Add(new TowerEntry((Cube)cube));
        }
    }
}
