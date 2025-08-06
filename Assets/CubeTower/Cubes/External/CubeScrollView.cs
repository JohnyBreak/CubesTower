using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cubes.UI
{
    public class CubeScrollView : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        private const float DragAngle = 70;
        [SerializeField] private Image _icon;

        private Action<int> _selectCallback;
        private Action<PointerEventData> _dragScrollCallback;
        private int _id;
        private bool _isDragging;
        
        public void Init(
            int id, 
            Sprite sprite, 
            Action<int> selectCallback, 
            Action<PointerEventData> dragScroll)
        {
            _id = id;
            _icon.sprite = sprite;
            _selectCallback = selectCallback;
            _dragScrollCallback = dragScroll;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.dragging == false)
            {
                return;
            }

            if (_isDragging)
            {
                return;
            }
            
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            
            var angle = Vector2.SignedAngle(Vector2.up, eventData.delta);
            if(angle < -DragAngle || angle > DragAngle)
            {
                _dragScrollCallback?.Invoke(eventData);
                return;
            }
            
            _isDragging = true;
            _selectCallback?.Invoke(_id);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}