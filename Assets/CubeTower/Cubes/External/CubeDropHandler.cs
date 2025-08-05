using Cubes;
using UnityEngine;

public class CubeDropHandler : MonoBehaviour
{
    [SerializeField] private ScrollWorldPosition _scrollWorldPosition;
    [SerializeField] private LayerMask _mask;
    
    public void Drop(Draggable target)
    {
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

        var collider = target.GetComponent<BoxCollider2D>();
        
        collider.enabled = false;
        var coll = Physics2D.OverlapBox(
            target.transform.position, 
            collider.bounds.size,
            0,
            _mask);
            
        collider.enabled = true;
            
        if (!coll)
        {
            return;
        }

        target.transform.position = coll.transform.position 
                                   + Vector3.up * target.transform.localScale.y * collider.size.y;
    }
}
