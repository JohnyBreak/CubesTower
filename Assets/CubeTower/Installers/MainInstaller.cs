using System;
using Cubes;
using Cubes.Config;
using CubeTower;
using Message;
using Pool;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private CubesConfig _config;
    [SerializeField] private RectTransform _scrollViewBorder;
    [SerializeField] private Camera _cam;
    [SerializeField] private MessageBox _messageBox;
    [SerializeField] private ScrollView _scrollView;
    [SerializeField] private LayerMaskProvider _layerMaskProvider;
    [SerializeField] private Dragger _dragger;
    //[SerializeField] private cube pool
    [SerializeField] private Hole _hole;
    
    public override void InstallBindings()
    {
        Container.Bind<Tower>().FromNew().AsSingle();
        Container.Bind<AssetProvider>().FromNew().AsSingle();
        Container.Bind<IDisposable>().To<AssetProvider>().FromResolve();
        Container.Bind<IconsProvider>().FromNew().AsSingle();
        Container.Bind<ITowerPredicate>().To<TestTowerPredicate>().FromNew().AsSingle();
       
        Container.Bind<CubePool>().FromNew().AsSingle();
        Container.Bind<LayerMaskProvider>().FromInstance(_layerMaskProvider).AsSingle();
        Container.Bind<Hole>().FromInstance(_hole).AsSingle();
        Container.Bind<Dragger>().FromInstance(_dragger).AsSingle();
        Container.Bind<ScrollView>().FromInstance(_scrollView).AsSingle();
        Container.Bind<MessageBox>().FromInstance(_messageBox).AsSingle();
        
        Container.Bind<CubeAnimator>().FromNew().AsSingle().NonLazy();
        Container.Bind<CubeFactory>().FromMethod(CreateCubeFactory).AsSingle().NonLazy();
        Container.Bind<CubeSpawner>().FromMethod(CreateCubeSpawner).AsSingle().NonLazy();
        Container.Bind<IDisposable>().To<CubeSpawner>().FromResolve();
        Container.Bind<CubeDropHandler>().FromMethod(CreateCubeDropHandler).AsSingle().NonLazy();
        Container.Bind<ScreenWorldUtility>().FromMethod(CreateScreenWorldUtility).AsSingle().NonLazy();
    }

    private CubeDropHandler CreateCubeDropHandler(InjectContext context)
    {
        var utility = context.Container.Resolve<ScreenWorldUtility>();
        var tower = context.Container.Resolve<Tower>();
        var animator = context.Container.Resolve<CubeAnimator>();
        var messageBox = context.Container.Resolve<MessageBox>();
        var pool = context.Container.Resolve<CubePool>();
        return new CubeDropHandler(tower, utility, animator, messageBox, pool);
    }

    private CubeFactory CreateCubeFactory(InjectContext context)
    {
        var iconsProvider = context.Container.Resolve<IconsProvider>();
        var assetProvider = context.Container.Resolve<AssetProvider>();
        var pool = context.Container.Resolve<CubePool>();
        return new CubeFactory(iconsProvider, assetProvider, pool, _config.Dtos);
    }

    private CubeSpawner CreateCubeSpawner(InjectContext context)
    {
        var utility = context.Container.Resolve<ScreenWorldUtility>();
        var factory = context.Container.Resolve<CubeFactory>();
        var dragger = context.Container.Resolve<Dragger>();
        var spawner = new CubeSpawner(_scrollView, utility, factory, dragger);
        return spawner;
    }

    private ScreenWorldUtility CreateScreenWorldUtility()
    {
        return new ScreenWorldUtility(_cam, _scrollViewBorder);
    }
}
