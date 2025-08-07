using Cubes;
using CubeTower;
using Localization;
using Message;
using UnityEngine;

public class CubeDropHandler
{
    private const string DestroyKey = "Cube destroyed";
    private const string HoleKey = "Cube fell into the hole";
    
    private readonly CubeAnimator _animator;
    private readonly MessageBox _messageBox;
    private readonly ScreenWorldUtility _screenWorldUtility;
    private readonly Tower _tower;

    public CubeDropHandler(
        Tower tower, 
        ScreenWorldUtility screenWorldUtility,
        CubeAnimator animator,
        MessageBox messageBox)
    {
        _tower = tower;
        _screenWorldUtility = screenWorldUtility;
        _animator = animator;
        _messageBox = messageBox;
    }

    public void Drop(Cube target)
    {
        if (!target)
        {
            return;
        }

        var scrollPos = _screenWorldUtility.GetScrollPosition();
            
        if (target.transform.position.y < scrollPos.y)
        {
            _messageBox.Show(DestroyKey.Localize());
            _animator.Destroy(target, () => Object.Destroy(target.gameObject));//TODO: return to pool
            return;
        }
            
        if (target.transform.position.x < 0)
        {
            _messageBox.Show(HoleKey.Localize());
            _animator.DropToHole(target, () => Object.Destroy(target.gameObject));
            return;
        }

        if (_tower.TrySet(target) == false)
        {
            _messageBox.Show(DestroyKey.Localize());
            _animator.Destroy(target, () => Object.Destroy(target.gameObject));
        }
    }
}
