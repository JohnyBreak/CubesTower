using UnityEngine;

namespace Cubes
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private CubeDropHandler _dropHandler;
        
        private Cube _currentCube;
        private Vector3 _dragOffset;
        private const float Speed = 1000;

        public void SetTarget(Cube target)
        {
            _currentCube = target;
            _dragOffset = _currentCube.transform.position - GetMousePos();
            _currentCube.OnDragStart();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _currentCube.OnDragEnd();
                _dropHandler.Drop(_currentCube);
                _currentCube = null;
            }

            if (Input.GetMouseButton(0) && _currentCube)
            {
                _currentCube.transform.position =
                    Vector3.MoveTowards(
                        _currentCube.transform.position, 
                        GetMousePos() + _dragOffset, 
                        Speed * Time.deltaTime);
                return;
            }

            if (_currentCube)
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

                if (rayHit.transform.TryGetComponent<Cube>(out var draggable))
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