using System;
using Cubes.Config;
using Cubes.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private CubesConfig _config;
    [SerializeField] private CubeScrollView _itemPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private ScrollRect _scrollRect;
    
    private Action<int> _selectCallback;
    
    private void Start()
    {
        foreach (var dto in _config.Dtos)
        {
            var cube = Instantiate(_itemPrefab, _parent);
            cube.Init(
                dto.ID,
                IconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName),
                OnSelect,
                DragScroll);
        }
    }

    public void Init(Action<int> selectCallback)
    {
        _selectCallback = selectCallback;
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
        _selectCallback?.Invoke(id);
    }
}
