using CubeTower.Common.Data;
using UnityEngine;
using Zenject;

public class ExitHandler : MonoBehaviour
{
    [Inject] private IDataManager _dataManager;
    
    private void OnApplicationQuit()
    {
        _dataManager.Save();
    }
}
