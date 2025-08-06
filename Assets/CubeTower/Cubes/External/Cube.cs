using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Cubes
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cube : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider;
        
        private Action<Cube> _onDragStartCallback;
        private Vector2 _size;
        
        public Vector2 Size => _size;
        
        private void Awake()
        {
            _size = new Vector2(
                _collider.size.x * transform.localScale.x, 
                _collider.size.y * transform.localScale.y);
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
            _onDragStartCallback?.Invoke(this);
        }
    }
}

