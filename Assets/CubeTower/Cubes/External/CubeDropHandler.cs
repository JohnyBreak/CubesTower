using Cubes;
using CubeTower;
using UnityEngine;

public class CubeDropHandler : MonoBehaviour
{
    [SerializeField] private ScrollWorldPosition _scrollWorldPosition;
    [SerializeField] private Tower _tower;
    
    public void Drop(Cube target)
    {
        if (!target)
        {
            return;
        }

        var scrollPos = _scrollWorldPosition.GetPosition();
            
        if (target.transform.position.y < scrollPos.y)
        {
            Destroy(target.gameObject);
            return;
        }
            
        if (target.transform.position.x < 0)
        {
            Destroy(target.gameObject); // drop to hole
            return;
        }

        if (_tower.TrySet(target) == false)
        {
            Destroy(target.gameObject);
        }
    }
}
