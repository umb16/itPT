using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    private StatContainer _statContainer;
    public Entity(StatContainer statContainer)
    {
        _statContainer = statContainer;

        Regen().Forget();
    }

    private async UniTask Regen()
    {
        await foreach (var _ in UniTaskAsyncEnumerable.Timer(new TimeSpan(), TimeSpan.FromSeconds(1)))
        {
            _statContainer.Void.Value += _statContainer.VoidRegen.Value;
        }
    }

}
