using Zenject;

namespace SampleGame
{
    public sealed class GameFieldInstaller : Installer<GameFieldView, GameFieldInstaller>
    {
        private readonly GameFieldView _view;        

        public GameFieldInstaller(GameFieldView view)
        {
            _view = view;
        }

        public override void InstallBindings()
        {
            Container.Bind<GameFieldView>().FromInstance(_view).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameFieldModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameFieldPresenter>().AsSingle().NonLazy();
        }
    }
}
