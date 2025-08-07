using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Transform _firstT;
    [SerializeField] private Transform _secondT;

    public Transform FirstT => _firstT;
    public Transform SecondT => _secondT;
}
