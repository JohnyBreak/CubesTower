using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cubes.UI
{
    public class CubeScrollView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _icon;

        private Action<int> _selectCallback;
        private int _id;
        
        public void Init(int id, Sprite sprite, Action<int> selectCallback)
        {
            _id = id;
            _icon.sprite = sprite;
            _selectCallback = selectCallback;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}