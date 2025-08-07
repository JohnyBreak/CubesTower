using UnityEngine;

public class LayerMaskProvider : MonoBehaviour
{
    [SerializeField] private LayerMask _cubeMask;
    
    public LayerMask CubeMask => _cubeMask;
}
