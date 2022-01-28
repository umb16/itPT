using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

public class NotMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var statContainer = new StatContainer(Random.Range(0,100), Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100), 0, 0);
        Container.Bind<StatContainer>().FromInstance(statContainer).AsSingle();
        Container.Bind<Entity>().AsSingle().NonLazy();
        Container.Bind<Hand>().AsSingle().NonLazy();
        Container.Bind<DisplayWriter>().AsSingle().NonLazy();
        Container.Bind<StatsVisualizer>().AsSingle().NonLazy();
        Container.Bind<CrosshairIconVisualizer>().AsSingle().NonLazy();
        Container.Bind<DisplayDebug>().AsSingle().NonLazy();
        Container.Bind<DisplayDataTime>().AsSingle().NonLazy();
    }
}