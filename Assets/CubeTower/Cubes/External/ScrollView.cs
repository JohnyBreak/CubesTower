using System;
using Cubes;
using Cubes.Config;
using Cubes.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ScrollView : MonoBehaviour
{
    public event Action<int> OnCubeSelectEvent;
    private const string CubeScrollViewPrefabKey = "CubeScrollView";
    private const string CubesConfigKey = "CubesConfig";
    
    [SerializeField] private Transform _parent;
    [SerializeField] private ScrollRect _scrollRect;
    
    private CubeScrollView _prefab;
    private Action<int> _selectCallback;
    
    [Inject]
    private void Init(AssetProvider assetProvider, IconsProvider iconsProvider)
    {
        _prefab = assetProvider.LoadAssetSync<GameObject>(CubeScrollViewPrefabKey).GetComponent<CubeScrollView>();
        var config = assetProvider.LoadAssetSync<CubesConfig>(CubesConfigKey);
        
        foreach (var dto in config.Dtos)
        {
            var cube = Instantiate(_prefab, _parent);
            cube.Init(
                dto.ID,
                iconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName),
                OnSelect,
                DragScroll);
        }
    }

    private void DragScroll(PointerEventData eventData)
    {
        eventData.pointerDrag = _scrollRect.gameObject;
        EventSystem.current.SetSelectedGameObject(_scrollRect.gameObject);
        _scrollRect.OnInitializePotentialDrag(eventData);
        _scrollRect.OnBeginDrag(eventData);
    }

    private void OnSelect(int id)
    {
        OnCubeSelectEvent?.Invoke(id);
    }
}
