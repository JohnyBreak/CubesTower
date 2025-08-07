using System;
using UnityEngine;

namespace Cubes
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cube : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private BoxCollider2D _collider;
        
        private Action<Cube> _onDragStartCallback;
        private Vector2 _size;
        private Vector3 _scale;
        private int _sortingOrder;
        
        public Vector2 Size => _size;
        
        private void Awake()
        {
            _sortingOrder = _spriteRenderer.sortingLayerID;
            _scale = transform.localScale;
            
            _size = new Vector2(
                _collider.size.x * transform.localScale.x, 
                _collider.size.y * transform.localScale.y);

            ToggleMaskable(false);
        }

        public void ToggleCollider(bool isActive)
        {
            _collider.enabled = isActive;
        }

        public void SetDragCallback(Action<Cube> onDragStartCallback)
        {
            _onDragStartCallback = onDragStartCallback;
        }

        public void OnDragStart()
        {
            _spriteRenderer.sortingOrder++;
            _onDragStartCallback?.Invoke(this);
        }

        public void ResetScale()
        {
            transform.localScale = _scale;
        }

        public void OnDragEnd()
        {
            _spriteRenderer.sortingOrder = _sortingOrder;
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void ToggleMaskable(bool showInMask)
        {
            _spriteRenderer.maskInteraction = showInMask
                ? SpriteMaskInteraction.VisibleInsideMask
                : SpriteMaskInteraction.None;
        }
    }
}

