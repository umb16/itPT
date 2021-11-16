using UnityEngine;
using Zenject;

public class FirstPersonControllerInstaller : MonoInstaller
{
    [SerializeField] private CrosshairRaycast _crosshairRaycast;
    public override void InstallBindings()
    {
        Container.Bind<CrosshairRaycast>().FromInstance(_crosshairRaycast).AsSingle();
    }
}