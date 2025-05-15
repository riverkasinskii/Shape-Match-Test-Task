using Common;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SampleGame
{    
    public sealed class FigureCreator : ITickable
    {               
        private readonly IFigureSpawner _spawner;
        private readonly FigureCatalog _catalog;
        private readonly Transform[] _figurePoints;
        private readonly Cooldown _cooldown;
        private readonly int _startCounter;


        private readonly List<IFigure> _spawnedFigures = new();
        private readonly FigureBehaviourController behaviourController = new();
        private FigureData _figureData;
        private const int MULTIPLICITY = 3;
        private int uniqFigureCount = 0;        
        private int _currentCounter = 0;

        public FigureCreator(IFigureSpawner spawner, FigureCatalog catalog, Transform[] figurePoints, Cooldown cooldown, int startCounter)
        {
            _spawner = spawner;
            _catalog = catalog;
            _figurePoints = figurePoints;
            _cooldown = cooldown;
            _startCounter = startCounter;            
        }
                
        public void DespawnActiveFigures()
        {
            int activeFigures = _spawner.GetNumActiveFigures();

            foreach (var item in _spawnedFigures)
            {
                if (item.GetFigureState())
                {
                    _spawner.Despawn((Figure)item);
                }                
            }
            _spawnedFigures.Clear();
            SetCounter(activeFigures);
        }

        private void SetCounter(int count)
        {
            _currentCounter -= count;
        }

        private void CreateFigure()
        {
            if (TryGetRandomFigure(out FigureData figureData))
            {
                _figureData = figureData;
            }

            IFigure figure = _spawner.Spawn(GetPosition(), Quaternion.identity);
            _spawnedFigures.Add(figure);

            TrySetExtraParamsInstall(figure);
            TryParamsInstall(figure);            
        }

        private void TrySetExtraParamsInstall(IFigure figure)
        {            
            behaviourController.InstallParams(figure, _figureData.BehaviourData);
        }

        private void TryParamsInstall(IFigure figure)
        {
            if (figure.GetFirstInstallState())
                return;
            
            figure.FirstInstallFigure(true);
            figure.InstallView(_figureData.Shape, _figureData.Color, _figureData.Animal);
            figure.InstallId(_figureData.id);

            if (_figureData is FigureBoxData box)
            {
                FigureBoxData.ColliderBoxData boxData = box.Collider;
                figure.TryInstallBoxColliderParams(boxData.Size);
            }
            if (_figureData is FigureCircleData circle)
            {
                FigureCircleData.ColliderCircleData boxData = circle.Collider;
                figure.TryInstallCircleColliderParams(boxData.Radius);
            }
        }

        void ITickable.Tick()
        {
            if (_currentCounter >= _startCounter)
                return;
            
            _cooldown.Tick(Time.deltaTime);
            if (!_cooldown.IsExpired())
            {
                return;
            }
            CreateFigure();
            _currentCounter++;            
            _cooldown.Reset();
        }

        private bool TryGetRandomFigure(out FigureData figureData)
        {            
            if (uniqFigureCount == 0)
            {
                int randomRange = Random.Range(0, _catalog.Count);
                figureData = _catalog.GetFigure(randomRange);
                uniqFigureCount++;
                return true;
            }
            uniqFigureCount++;

            if (uniqFigureCount == MULTIPLICITY)
            {
                uniqFigureCount = 0;
            }            
            figureData = null;
            return false;
        }

        private Vector2 GetPosition()
        {
            int randomRange = Random.Range(0, _figurePoints.Length);
            return _figurePoints[randomRange].position;
        }                
    }
}

