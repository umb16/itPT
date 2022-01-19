using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StatContainer
{
    public readonly AsyncReactiveProperty<float> Health = new AsyncReactiveProperty<float>(0);
    public readonly AsyncReactiveProperty<float> Cheerfulness = new AsyncReactiveProperty<float>(0);
    public readonly AsyncReactiveProperty<float> Energy = new AsyncReactiveProperty<float>(0);
    public readonly AsyncReactiveProperty<float> Water = new AsyncReactiveProperty<float>(0);
    public readonly AsyncReactiveProperty<float> Cold = new AsyncReactiveProperty<float>(0);
    public readonly AsyncReactiveProperty<float> Void = new AsyncReactiveProperty<float>(0);

    public StatContainer(float health, float cheerfulness, float energy, float water, float cold, float @void)
    {
        Health.Value = health;
        Cheerfulness.Value = cheerfulness;
        Energy.Value = energy;
        Water.Value = water;
        Cold.Value = cold;
        Void.Value = @void;
    }

}
