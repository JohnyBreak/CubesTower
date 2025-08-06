using Cubes;
using UnityEngine;

public class CubeDropHandler : MonoBehaviour
{
    [SerializeField] private ScrollWorldPosition _scrollWorldPosition;
    [SerializeField] private LayerMask _mask;
    
    public void Drop(Draggable target)
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
            Destroy(target.gameObject); 
            return;
        }

        target.Collider.enabled = false;
        var coll = Physics2D.OverlapBox(
            target.transform.position, 
            target.Collider.bounds.size,
            0,
            _mask);
            
        target.Collider.enabled = true;
            
        if (!coll)
        {
            return;
        }

        target.transform.position = coll.transform.position 
                                   + Vector3.up * (target.transform.localScale.y * target.Collider.size.y);
    }
}
