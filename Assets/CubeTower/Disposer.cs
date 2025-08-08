using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CubeTower
{
    public class Disposer : MonoBehaviour
    {
        [Inject] private List<IDisposable> _disposables;

        private void OnDestroy()
        {
            if (_disposables == null)
            {
                return;
            }

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
}