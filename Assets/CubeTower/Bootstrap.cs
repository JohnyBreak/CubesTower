using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    [Inject] private List<IInitableEntity> _entities;

    private IEnumerator Start()
    {
        yield return null;
        var ordered = _entities.OrderBy(x => x.GetOrder());

        foreach (var entity in ordered)
        {
            entity.Init();
        }
    }
}
