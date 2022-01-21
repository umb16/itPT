using System.Threading;
using UnityEngine;
using Zenject;

public class DisplayInstaller : MonoInstaller
{
    [SerializeField] private Display _display;
    public override void InstallBindings()
    {
        Container.Bind<CancellationTokenSource>().AsSingle();
        Container.Bind<Display>().FromInstance(_display).AsSingle();
    }
}