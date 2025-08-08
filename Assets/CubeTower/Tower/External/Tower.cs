using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cubes;
using CubeTower.Common.Data;
using Localization;
using Message;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeTower
{
    public class Tower
    {
        private const string TopLimitMessageKey = "Top screen limit";
        private const string SpawnedKey = "Cube spawned";

        private readonly TowerData _data;
        private readonly ScreenWorldUtility _screenWorldUtility;
        private readonly CubeAnimator _animator;
        private readonly MessageBox _messageBox;
        private readonly LayerMaskProvider _layerMaskProvider;

        private TowerList<Cube> _listNodes = new();
        private List<ITowerPredicate> _placementPredicates = new();

        public TowerList<Cube> Nodes => _listNodes;
        
        public Tower(
            ScreenWorldUtility screenWorldUtility, 
            CubeAnimator animator, 
            MessageBox messageBox,
            LayerMaskProvider layerMaskProvider,
            List<ITowerPredicate> predicates,
            IDataManager dataManager)
        {
            _screenWorldUtility = screenWorldUtility;
            _animator = animator;
            _messageBox = messageBox;
            _layerMaskProvider = layerMaskProvider;
            _placementPredicates = predicates;

            _data = dataManager.GetData(nameof(TowerData)) as TowerData;
            if (_data == null)
            {
                Debug.LogError("TowerData == null");
            }
        }

        public bool TrySet(Cube cube)
        {
            var rightUpCornerPos = _screenWorldUtility.GetRightUpCornerPosition();
            
            if (_listNodes.Count < 1)
            {
                var newPos = cube.transform.position;
                
                var xPos = cube.transform.position.x + (cube.Size.x);
                if (xPos > rightUpCornerPos.x)
                {
                    newPos.x = rightUpCornerPos.x - (cube.Size.x);
                }
                
                var yPos = cube.transform.position.y + (cube.Size.y / 2);
                if (yPos > rightUpCornerPos.y)
                {
                    newPos.y = rightUpCornerPos.y - (cube.Size.y / 2);
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
                _layerMaskProvider.CubeMask);
            
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

            if (CanBePlaced(cube) == false)
            {
                return false;
            }

            var cubeOnTop = _listNodes.Tail.Data;
            
            var shift = Random.Range(-(cube.Size.x / 2), (cube.Size.x / 2));

            Vector3 jumpPosition = new Vector3(cubeOnTop.transform.position.x + shift,
                cubeOnTop.transform.position.y + cube.Size.y,
                cube.transform.position.z);
            
            _animator.JumpTo(cube, jumpPosition);
            
            AddToList(cube);
            return true;
        }

        public void AddSilent(Cube cube)
        {
            cube.SetDragCallback(OnCubeDrag);
            _listNodes.Add(cube);
        }

        private bool CanBePlaced(Cube cube)
        {
            foreach (var predicate in _placementPredicates)
            {
                if (predicate == null)
                {
                    continue;
                }

                if (predicate?.Can(cube) == false)
                {
                    return false;
                }
            }
            
            return true;
        }

        private void AddToList(Cube cube)
        {
            AddSilent(cube);
            _messageBox.Show(SpawnedKey.Localize());
        }

        private bool CanBeSetOnTop()
        {
            var cube = _listNodes.Tail.Data;

            var yPos = cube.transform.position.y + (cube.Size.y * 0.75f);

            return yPos <  _screenWorldUtility.GetRightUpCornerPosition().y;
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
