using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Cubes
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Draggable : MonoBehaviour
    {
        private const float Speed = 1000;

        [SerializeField] private ScrollWorldPosition _scrollWorldPosition;
        [SerializeField] private LayerMask _mask;
        
        private float _offset;
        private Vector3 _dragOffset;
        private Camera _cam;
        private BoxCollider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            _offset = _collider.size.y;
            
            _cam = Camera.main;
        }

        private void OnMouseDown()
        {
            _dragOffset = transform.position - GetMousePos();
        }

        private void OnMouseUp()
        {
            var scrollPos = _scrollWorldPosition.GetPosition();
            
            if (transform.position.y < scrollPos.y)
            {
                Destroy(gameObject);
                return;
            }
            
            if (transform.position.x < 0)
            {
                Destroy(gameObject); 
                return;
            }

            _collider.enabled = false;
            var coll = Physics2D.OverlapBox(
                transform.position, 
                _collider.bounds.size,
                0,
                _mask);
            
            _collider.enabled = true;
            
            if (!coll)
            {
                return;
            }

            transform.position = coll.transform.position 
                                 + Vector3.up * transform.localScale.y * _offset;
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

