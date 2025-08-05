using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Cubes
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Draggable : MonoBehaviour
    {
        private const float Speed = 1000;
        [SerializeField] private CubeDropHandler _dropHandler;
        
        private Vector3 _dragOffset;
        private Camera _cam;
        private BoxCollider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _cam = Camera.main;
        }

        private void OnMouseDown()
        {
            _dragOffset = transform.position - GetMousePos();
        }

        private void OnMouseUp()
        {
            _dropHandler.Drop(this);
        }

        private void OnMouseDrag()
        {
            transform.position =
                Vector3.MoveTowards(
                    transform.position, 
                    GetMousePos() + _dragOffset, 
                    Speed * Time.deltaTime);
        }
        
        private Vector3 GetMousePos()
        {
            var position = _cam.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            return position;
        }
    }
}

