using UnityEngine;
using Zenject;

namespace Cubes
{
    public class Dragger : MonoBehaviour
    {
        private CubeDropHandler _dropHandler;
        private ScreenWorldUtility _utility;
        private LayerMaskProvider _layerMaskProvider;
        
        private Cube _currentCube;
        private Vector3 _dragOffset;
        private const float Speed = 1000;

        [Inject]
        private void Init(
            CubeDropHandler dropHandler, 
            ScreenWorldUtility utility,
            LayerMaskProvider layerMaskProvider)
        {
            _dropHandler = dropHandler;
            _utility = utility;
            _layerMaskProvider = layerMaskProvider;
        }

        public void SetTarget(Cube target)
        {
            _currentCube = target;
            _dragOffset = _currentCube.transform.position - _utility.GetMouseWorldPosition();
            _currentCube.OnDragStart();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_currentCube != null)
                {
                    _currentCube.OnDragEnd();
                    _dropHandler.Drop(_currentCube);
                    _currentCube = null;
                }
            }

            if (Input.GetMouseButton(0) && _currentCube)
            {
                _currentCube.transform.position =
                    Vector3.MoveTowards(
                        _currentCube.transform.position, 
                        _utility.GetMouseWorldPosition() + _dragOffset, 
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
                    _utility.GetScreenPointToRay(), 
                    10000, 
                    _layerMaskProvider.CubeMask);
            
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
    }
}