using Cubes;
using UnityEngine;

namespace CubeTower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private LayerMask _mask;
        private TowerList<Cube> _listNodes = new();

        public bool TrySet(Cube cube)
        {
            if (_listNodes.Count < 1)
            {
                //TODO: check screen border
                AddToList(cube);
                return true;
            }

            cube.Collider.enabled = false;
            var coll = Physics2D.OverlapBox(
                cube.transform.position, 
                cube.Collider.bounds.size,
                0,
                _mask);
            
            cube.Collider.enabled = true;
            
            if (!coll)
            {
                return false;
            }
        
            //if(predicates == false) return false

            var cubeOnTop = _listNodes.Tail.Data;
            
            // random X shift
            cube.transform.position = cubeOnTop.transform.position 
                                      + Vector3.up * (cube.transform.localScale.y * cubeOnTop.Collider.size.y);

            AddToList(cube);
            return true;
        }

        private void AddToList(Cube cube)
        {
            cube.SetDragCallback(OnCubeDrag);
            _listNodes.Add(cube);
        }

        private void OnCubeDrag(Cube cube)
        {
            _listNodes.Remove(cube);
        }
    }
}
