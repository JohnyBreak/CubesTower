using System;
using Cubes.Config;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Cubes.UI
{
    public class ScrollView : MonoBehaviour
    {
        public event Action<int> OnCubeSelectEvent;
    
        private const string CubeScrollViewPrefabKey = "CubeScrollView";
    
        [SerializeField] private Transform _parent;
        [SerializeField] private ScrollRect _scrollRect;
    
        private CubeScrollView _prefab;
        private Action<int> _selectCallback;
    
        [Inject]
        private void Init(
            AssetProvider assetProvider, 
            IconsProvider iconsProvider,
            ConfigContainer container)
        {
            _prefab = assetProvider.LoadAssetSync<GameObject>(CubeScrollViewPrefabKey).GetComponent<CubeScrollView>();

            if (container.CubesDtos == null)
            {
                return;
            }

            foreach (var dto in container.CubesDtos.Dtos)
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
}
