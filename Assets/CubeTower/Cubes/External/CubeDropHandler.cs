using Cubes;
using CubeTower;
using Localization;
using Message;
using UnityEngine;

public class CubeDropHandler : MonoBehaviour
{
    private const string DestroyKey = "Cube destroyed";
    private const string HoleKey = "Cube fell into the hole";
    
    [SerializeField] private CubeAnimator _animator;
    [SerializeField] private MessageBox _messageBox;
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
            _messageBox.Show(DestroyKey.Localize());
            _animator.Destroy(target, () => Destroy(target.gameObject));
            return;
        }
            
        if (target.transform.position.x < 0)
        {
            _messageBox.Show(HoleKey.Localize());
            _animator.DropToHole(target, () => Destroy(target.gameObject));
            return;
        }

        if (_tower.TrySet(target) == false)
        {
            _messageBox.Show(DestroyKey.Localize());
            _animator.Destroy(target, () => Destroy(target.gameObject));
        }
    }
}
