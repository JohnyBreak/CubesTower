using CubeTower.Common.Data;
using UnityEngine;
using Zenject;

public class ExitHandler : MonoBehaviour
{
    [Inject] private IDataManager _dataManager;
    [Inject] private TowerLoader _towerLoader;
    
    private void OnApplicationQuit()
    {
        _towerLoader?.Save();
        _dataManager?.Save();
    }
}
