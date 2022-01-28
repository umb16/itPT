using UnityEngine;
using Zenject;

public class FirstPersonControllerInstaller : MonoInstaller
{
    [SerializeField] private CrosshairRaycast _crosshairRaycast;
    [SerializeField] private InHandPoint _inHandPoint;
    public override void InstallBindings()
    {
        Container.Bind<CrosshairRaycast>().FromInstance(_crosshairRaycast).AsSingle();
        Container.Bind<InHandPoint>().FromInstance(_inHandPoint).AsSingle();
    }
}