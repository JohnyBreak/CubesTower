using UnityEngine;

public class ScreenWorldUtility
{
    private readonly Camera _cam;
    private readonly RectTransform _scrollBorderTransform;

    public ScreenWorldUtility(Camera cam, RectTransform scrollBorderTransform)
    {
        _cam = cam;
        _scrollBorderTransform = scrollBorderTransform;
    }

    public Vector3 GetMouseWorldPosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public Vector3 GetScrollPosition()
    {
        Vector3 position = Vector3.zero;
        return _scrollBorderTransform.TransformPoint(position);
    }

    public Vector3 GetRightUpCornerPosition()
    {
        return _cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    public Ray GetScreenPointToRay()
    {
        return _cam.ScreenPointToRay(Input.mousePosition);
    }
}
