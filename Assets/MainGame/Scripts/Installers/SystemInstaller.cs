using Common;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class SystemInstaller : MonoInstaller
    {
        [Header("Figure Installer Params")]
        [SerializeField]
        private FigureCatalog _catalog;

        [SerializeField]
        private Figure _figurePrefab;

        [SerializeField]
        private Transform _poolContainer;

        [SerializeField]
        private Transform[] _spawnPoints;

        [SerializeField]
        private int _startFigures = 10;

        [SerializeField]
        private float _duration = 0.1f;

        [Header("Game Field Installer Params")]
        [SerializeField]
        private GameFieldView _view;

        [Header("Popup Shower Params")]
        [SerializeField]
        private PopupShower _popupShower;

        public override void InstallBindings()
        {
            FigureInstaller.Install(Container, _figurePrefab, _poolContainer, _catalog);
            GameFieldInstaller.Install(Container, _view);

            Container
                .BindInterfacesAndSelfTo<FigureCreator>()
                .AsSingle()
                .WithArguments(_spawnPoints, _startFigures, new Cooldown(_duration, _duration))
                .NonLazy();

            Container.Bind<PopupShower>().FromInstance(_popupShower).AsSingle().NonLazy();
        }
    }
}
