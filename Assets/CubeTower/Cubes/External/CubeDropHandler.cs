using Cubes;
using CubeTower;
using Localization;
using Message;
using Pool;

public class CubeDropHandler
{
    private const string DestroyKey = "Cube destroyed";
    private const string HoleKey = "Cube fell into the hole";
    
    private readonly CubeAnimator _animator;
    private readonly MessageBox _messageBox;
    private readonly CubePool _pool;
    private readonly ScreenWorldUtility _screenWorldUtility;
    private readonly Tower _tower;

    public CubeDropHandler(
        Tower tower, 
        ScreenWorldUtility screenWorldUtility,
        CubeAnimator animator,
        MessageBox messageBox,
        CubePool pool)
    {
        _tower = tower;
        _screenWorldUtility = screenWorldUtility;
        _animator = animator;
        _messageBox = messageBox;
        _pool = pool;
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
            _animator.Destroy(target, () => _pool.BackObjectToPool(target));
            return;
        }
            
        if (target.transform.position.x < 0)
        {
            _messageBox.Show(HoleKey.Localize());
            _animator.DropToHole(target, () => _pool.BackObjectToPool(target));
            return;
        }

        if (_tower.TrySet(target) == false)
        {
            _messageBox.Show(DestroyKey.Localize());
            _animator.Destroy(target, () => _pool.BackObjectToPool(target));
            
        }
    }
}
