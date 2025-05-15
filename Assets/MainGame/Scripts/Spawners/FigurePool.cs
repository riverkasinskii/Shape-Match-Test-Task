using Zenject;
using UnityEngine;
using System;

namespace SampleGame
{
    public sealed class FigurePool : MonoMemoryPool<Vector3, Quaternion, Figure>, IFigureSpawner
    {       
        public event Action<Figure> OnFigureClicked;
        public event Action OnNumActiveIsZero;

        protected override void Reinitialize(Vector3 position, Quaternion rotation, Figure item)
        {
            item.SetPositionAndRotation(position, rotation);            
        }

        protected override void OnSpawned(Figure item)
        {
            base.OnSpawned(item);            
            item.OnDispose += OnDispose;
            item.OnFigureClicked += FigureClicked;
        }

        protected override void OnDespawned(Figure item)
        {            
            base.OnDespawned(item);            
            item.OnDispose -= OnDispose;
            item.OnFigureClicked -= FigureClicked;
        }

        private void OnDispose(Figure figure)
        {
            Despawn(figure);
            CheckActiveFigures();
        }

        private void CheckActiveFigures()
        {
            if (NumActive == 0)
            {
                OnNumActiveIsZero?.Invoke();
            }
        }

        private void FigureClicked(Figure item)
        {
            OnFigureClicked?.Invoke(item);            
        }
                
        IFigure IFigureSpawner.Spawn(Vector3 position, Quaternion rotation)
        {            
            return Spawn(position, rotation);            
        }

        void IFigureSpawner.Despawn(Figure item)
        {
            Despawn(item);
        }

        int IFigureSpawner.GetNumActiveFigures()
        {
            return NumActive;
        }
    }
}
