using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Enemy;
using CodeBase.Services.Level;
using CodeBase.Services.StaticData;
using CodeBase.Services.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private GameObject _curtainPrefab;

    public override void InstallBindings()
    {
      LoadingCurtain curtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(_curtainPrefab);
      Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromNewComponentOnNewGameObject().AsSingle();
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      Container.Bind<SceneLoader>().AsSingle();
      Container.Bind<LoadingCurtain>().FromInstance(curtain).AsSingle();
      Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
      Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
      Container.BindInterfacesAndSelfTo<EnemyDeathService>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
    }
  }
}