using UnityEngine;

namespace Cubes
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private LayerMask _mask;
        
        private Transform _currentDraggable;
        private Vector3 _dragOffset;
        private const float Speed = 1000;

        public void SetTarget(Draggable target)
        {
            
        }

        private void OnMouseDown()
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(
                _cam.ScreenPointToRay(Input.mousePosition), 
                0, 
                _mask);
            
            if (!rayHit.transform)
            {
                return;
            }

            _currentDraggable = rayHit.transform;
                _dragOffset = _currentDraggable.position - GetMousePos();
        }

        private void OnMouseDrag()
        {
            if (_currentDraggable == null)
            {
                return;
            }

            _currentDraggable.position =
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