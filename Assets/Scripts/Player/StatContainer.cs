using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StatContainer
{
    public readonly Stat Health;
    public readonly Stat Cheerfulness;
    public readonly Stat Energy;
    public readonly Stat Water;
    public readonly Stat Cold;
    public readonly Stat Void;
    public readonly Stat VoidRegen = new Stat(1f, 10, ' ');
    public readonly Stat[] Stats;

    public StatContainer(float health, float cheerfulness, float energy, float water, float cold, float @void)
    {
        Health = new Stat(health, 100, 'h');
        Cheerfulness = new Stat(cheerfulness, 100, 'M');
        Energy = new Stat(energy, 100, 'E');
        Water = new Stat(energy, 100, 'D');
        Cold = new Stat(energy, 100, 'S');
        Void = new Stat(energy, 100, 's');
        Stats = new Stat[] { Health, Cheerfulness, Energy, Water, Cold, Void};
    }

    public void Update()
    {
        foreach (var stat in Stats)
        {
            stat.Update();
        }
    }

}
