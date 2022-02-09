using UnityEngine;
using Zenject;

public class LocalizationInstaller : MonoInstaller
{
    [SerializeField] private LocalizationManager _localizationManager;
    public override void InstallBindings()
    {
        Container.Bind<LocalizationManager>().FromInstance(_localizationManager).AsSingle();
    }
}