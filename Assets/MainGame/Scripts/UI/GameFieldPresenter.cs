using System;
using System.Collections.Generic;
using Zenject;

namespace SampleGame
{
    public sealed class GameFieldPresenter : IInitializable, IDisposable
    {
        private readonly GameFieldView _view;
        private readonly GameFieldModel _model;
        private readonly FigureCreator _creator;

        public GameFieldPresenter(GameFieldView view, GameFieldModel model, FigureCreator creator)
        {
            _view = view;
            _model = model;
            _creator = creator;
        }

        void IInitializable.Initialize()
        {
            List<Cell> cells = _view.GetCells();
            _model.InstallCells(cells);

            _view.OnFigureClicked += OnFigureClicked;
            _view.OnResetClicked += OnResetClicked;
            _model.OnLoseGameEvent += OnLoseGameEvent;
        }

        void IDisposable.Dispose()
        {
            _view.OnFigureClicked -= OnFigureClicked;
            _view.OnResetClicked -= OnResetClicked;
            _model.OnLoseGameEvent -= OnLoseGameEvent;
        }

        private void OnLoseGameEvent()
        {
            _view.Lose();
        }

        private void OnResetClicked()
        {            
            _creator.DespawnActiveFigures();         
        }

        private void OnFigureClicked(Figure figure)
        {            
            _model.SetupFigureToEmptyCell(figure.GetView(), figure.GetId());
        }
    }
}
