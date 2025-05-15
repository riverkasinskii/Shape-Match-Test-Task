using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class FigureInstaller : Installer<Figure, Transform, FigureCatalog, FigureInstaller>
    {        
        private readonly Figure _figurePrefab;        
        private readonly Transform _transform;
        private readonly FigureCatalog _catalog;

        public FigureInstaller(Figure figurePrefab, Transform transform, FigureCatalog catalog)
        {
            _figurePrefab = figurePrefab;
            _transform = transform;
            _catalog = catalog;
        }

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<Figure, FigurePool>()
                .WithInitialSize(30)
                .ExpandByOneAtATime()  
                .FromComponentInNewPrefab(_figurePrefab)
                .UnderTransform(_transform)
                .AsCached();

            Container.Bind<IFigureSpawner>().To<FigurePool>().FromResolve();
            Container.BindInterfacesAndSelfTo<FigureCatalog>().FromInstance(_catalog).AsCached().NonLazy();
        }
    }
}
