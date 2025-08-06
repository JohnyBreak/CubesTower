using System;
using System.Collections;
using Cubes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeTower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private LayerMask _mask;
        
        private TowerList<Cube> _listNodes = new();
        private Vector3 _rightUpCornerPos;
        
        private IEnumerator Start()
        {
            yield return null;
            _rightUpCornerPos = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }

        public bool TrySet(Cube cube)
        {
            if (_listNodes.Count < 1)
            {
                var newPos = cube.transform.position;
                
                var xPos = cube.transform.position.x + (cube.Size.x / 2);
                if (xPos > _rightUpCornerPos.x)
                {
                    newPos.x = _rightUpCornerPos.x - (cube.Size.x);
                }
                
                var yPos = cube.transform.position.y + (cube.Size.y / 2);
                if (yPos > _rightUpCornerPos.y)
                {
                    newPos.y = _rightUpCornerPos.y - (cube.Size.y / 2);
                }

                cube.transform.position = newPos;
                
                AddToList(cube);
                return true;
            }

            cube.ToggleCollider(false);
            var coll = Physics2D.OverlapBox(
                cube.transform.position, 
                cube.Size,
                0,
                _mask);
            
            cube.ToggleCollider(true);
            
            if (!coll)
            {
                return false;
            }

            if (CanBeSetOnTop() == false)
            {
                return false;
            }
            
            //if(predicates == false) return false

            var cubeOnTop = _listNodes.Tail.Data;
            
            var shift = Random.Range(-(cube.Size.x / 2), (cube.Size.x / 2));
            cube.transform.position =
                new Vector3(cubeOnTop.transform.position.x + shift, 
                    cubeOnTop.transform.position.y + cube.Size.y,
                    cube.transform.position.z);

            AddToList(cube);
            return true;
        }

        private void AddToList(Cube cube)
        {
            cube.SetDragCallback(OnCubeDrag);
            _listNodes.Add(cube);
        }

        private bool CanBeSetOnTop()
        {
            var cube = _listNodes.Tail.Data;

            var yPos = cube.transform.position.y + (cube.Size.y / 2);

            return yPos < _rightUpCornerPos.y;
        }

        private void OnCubeDrag(Cube cube)
        {
            _listNodes.Remove(cube);
        }
    }
}
