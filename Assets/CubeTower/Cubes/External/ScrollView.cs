using Cubes.Config;
using Cubes.UI;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private CubesConfig _config;
    [SerializeField] private CubeScrollView _itemPrefab;
    [SerializeField] private Transform _parent;
    
    private void Start()
    {
        foreach (var dto in _config.Dtos)
        {
            var cube = Instantiate(_itemPrefab, _parent);
            cube.Init(
                dto.ID,
                IconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName),
                OnSelect);
        }
    }

    private void OnSelect(int id)
    {
        
    }
}
