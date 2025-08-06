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
        public BoxCollider2D Collider => _collider;

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

