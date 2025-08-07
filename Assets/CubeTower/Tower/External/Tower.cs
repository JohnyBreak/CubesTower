using System.Collections;
using System.Linq;
using Cubes;
using Localization;
using Message;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeTower
{
    public class Tower : MonoBehaviour
    {
        private const string TopLimitMessageKey = "Top screen limit";
        private const string SpawnedKey = "Cube spawned";
        
        [SerializeField] private CubeAnimator _animator;
        [SerializeField] private MessageBox _messageBox;
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
                
                var xPos = cube.transform.position.x + (cube.Size.x);
                if (xPos > _rightUpCornerPos.x)
                {
                    newPos.x = _rightUpCornerPos.x - (cube.Size.x);
                }
                
                var yPos = cube.transform.position.y + (cube.Size.y / 2);
                if (yPos > _rightUpCornerPos.y)
                {
                    newPos.y = _rightUpCornerPos.y - (cube.Size.y / 2);
                }

                _animator.MoveTo(cube, newPos);
                AddToList(cube);
                return true;
            }

            cube.ToggleCollider(false);
            var coll = Physics2D.OverlapBox(
                cube.transform.position, 
                cube.Size * 0.5f,
                0,
                _mask);
            
            cube.ToggleCollider(true);
            
            if (!coll)
            {
                return false;
            }

            if (CanBeSetOnTop() == false)
            {
                _messageBox.Show(TopLimitMessageKey.Localize(), 1);
                return false;
            }
            
            //if(predicates == false) return false

            var cubeOnTop = _listNodes.Tail.Data;
            
            var shift = Random.Range(-(cube.Size.x / 2), (cube.Size.x / 2));

            Vector3 jumpPosition = new Vector3(cubeOnTop.transform.position.x + shift,
                cubeOnTop.transform.position.y + cube.Size.y,
                cube.transform.position.z);
            
            _animator.JumpTo(cube, jumpPosition);
            
            AddToList(cube);
            return true;
        }

        private void AddToList(Cube cube)
        {
            cube.SetDragCallback(OnCubeDrag);
            _listNodes.Add(cube);
            _messageBox.Show(SpawnedKey.Localize());
        }

        private bool CanBeSetOnTop()
        {
            var cube = _listNodes.Tail.Data;

            var yPos = cube.transform.position.y + (cube.Size.y * 0.75f);

            return yPos < _rightUpCornerPos.y;
        }

        private void OnCubeDrag(Cube cube)
        {
            DropTopCubes(cube);
            _listNodes.Remove(cube);
        }

        private void DropTopCubes(Cube cube)
        {
            var cubesToDrop = _listNodes.GetDatasAfter(cube).ToList();

            _animator.DropCubesDown(cubesToDrop);
        }
    }
}
