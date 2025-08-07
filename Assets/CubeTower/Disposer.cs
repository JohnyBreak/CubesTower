using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Disposer : MonoBehaviour
{
    [Inject] private List<IDisposable> _disposables;
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            if (disposable is SceneContextRegistryAdderAndRemover)
            {
                continue;
            }

            disposable.Dispose();
        }
    }
}
