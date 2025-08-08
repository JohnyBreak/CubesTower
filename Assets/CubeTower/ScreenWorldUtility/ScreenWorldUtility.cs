using UnityEngine;

public class ScreenWorldUtility : IInitableEntity
{
    private readonly Camera _cam;
    private readonly RectTransform _scrollBorderTransform;

    private Vector3 _rightUpCornerPosition;
    private Vector3 _scrollBorderPosition;
    
    public ScreenWorldUtility(Camera cam, RectTransform scrollBorderTransform)
    {
        _cam = cam;
        _scrollBorderTransform = scrollBorderTransform;
    }

    public int GetOrder()
    {
        return -1;
    }

    public void Init()
    {
        _rightUpCornerPosition = _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        Vector3 position = Vector3.zero;
        _scrollBorderPosition = _scrollBorderTransform.TransformPoint(position);
    }
    
    public Vector3 GetMouseWorldPosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public Vector3 GetScrollPosition()
    {
        return _scrollBorderPosition;
    }

    public Vector3 GetRightUpCornerPosition()
    {
        return _rightUpCornerPosition;
    }

    public Ray GetScreenPointToRay()
    {
        return _cam.ScreenPointToRay(Input.mousePosition);
    }
}
