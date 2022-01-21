using UnityEngine;
using Zenject;

public class NotMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var statContainer = new StatContainer(20, 20, 20, 20, 0, 0);
        Container.Bind<StatContainer>().FromInstance(statContainer).AsSingle();
        Container.Bind<DisplayWriter>().AsSingle().NonLazy();
        Container.Bind<StatsVisualizer>().AsSingle().NonLazy();
        Container.Bind<Entity>().AsSingle().NonLazy();
    }
}