using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Cubes
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Draggable : MonoBehaviour
    {
        private Vector3 _dragOffset;
        [SerializeField] private BoxCollider2D _collider;

        public BoxCollider2D Collider => _collider;
    }
}

