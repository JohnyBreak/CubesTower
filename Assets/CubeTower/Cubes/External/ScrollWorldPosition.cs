using UnityEngine;

public class ScrollWorldPosition : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        Vector3 position = Vector3.zero;
        return ((RectTransform)transform).TransformPoint(position);
    }
}
