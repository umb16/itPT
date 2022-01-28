using UnityEngine;
using VHS;
using Zenject;

public class FirstPersonControllerInstaller : MonoInstaller
{
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private CrosshairRaycast _crosshairRaycast;
    [SerializeField] private InHandPoint _inHandPoint;
    public override void InstallBindings()
    {
        Container.Bind<FirstPersonController>().FromInstance(_firstPersonController).AsSingle();
        Container.Bind<CrosshairRaycast>().FromInstance(_crosshairRaycast).AsSingle();
        Container.Bind<InHandPoint>().FromInstance(_inHandPoint).AsSingle();
    }
}