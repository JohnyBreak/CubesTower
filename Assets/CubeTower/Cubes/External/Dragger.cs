using UnityEngine;

namespace Cubes
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private CubeDropHandler _dropHandler;
        
        private Draggable _currentDraggable;
        private Vector3 _dragOffset;
        private const float Speed = 1000;

        public void SetTarget(Draggable target)
        {
            _currentDraggable = target;
            _dragOffset = _currentDraggable.transform.position - GetMousePos();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _dropHandler.Drop(_currentDraggable);
                _currentDraggable = null;
            }

            if (Input.GetMouseButton(0) && _currentDraggable)
            {
                _currentDraggable.transform.position =
                    Vector3.MoveTowards(
                        _currentDraggable.transform.position, 
                        GetMousePos() + _dragOffset, 
                        Speed * Time.deltaTime);
                return;
            }

            if (_currentDraggable)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(
                    _cam.ScreenPointToRay(Input.mousePosition), 
                    10000, 
                    _mask);
            
                if (!rayHit.collider)
                {
                    return;
                }

                if (rayHit.transform.TryGetComponent<Draggable>(out var draggable))
                {
                    SetTarget(draggable);
                }
            }
        }

        private Vector3 GetMousePos()
        {
            var position = _cam.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            return position;
        }
    }
}