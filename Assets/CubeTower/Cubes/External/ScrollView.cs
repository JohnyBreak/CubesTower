using System;
using Cubes.Config;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Cubes.UI
{
    public class ScrollView : MonoBehaviour, IInitableEntity
    {
        public event Action<int> OnCubeSelectEvent;
    
        private const string CubeScrollViewPrefabKey = "CubeScrollView";
    
        [SerializeField] private Transform _parent;
        [SerializeField] private ScrollRect _scrollRect;

        private AssetProvider _assetProvider;
        private IconsProvider _iconsProvider;
        private ConfigContainer _container;
        
        private CubeScrollView _prefab;
        private Action<int> _selectCallback;
    
        [Inject]
        private void Init(
            AssetProvider assetProvider, 
            IconsProvider iconsProvider,
            ConfigContainer container)
        {
            _assetProvider = assetProvider;
            _iconsProvider = iconsProvider;
            _container = container;
        }

        public int GetOrder()
        {
            return 0;
        }

        public void Init()
        {
            _prefab = _assetProvider.LoadAssetSync<GameObject>(CubeScrollViewPrefabKey).GetComponent<CubeScrollView>();

            if (_container.CubesDtos == null)
            {
                return;
            }

            foreach (var dto in _container.CubesDtos.Dtos)
            {
                var cube = Instantiate(_prefab, _parent);
                cube.Init(
                    dto.ID,
                    _iconsProvider.GetSprite(CubesConfig.SpriteSheetName, dto.SpriteName),
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
