using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SampleGame
{
    public sealed class GameFieldView : MonoBehaviour
    {
        public event Action<Figure> OnFigureClicked;
        public event Action OnResetClicked;

        [SerializeField] private List<Cell> Cells;
        [SerializeField] private Button resetFieldButton;

        private FigurePool _pool;
        private PopupShower _popupShower;

        [Inject]
        public void Construct(FigurePool pool, PopupShower popupShower)
        {
            _pool = pool;           
            _popupShower = popupShower;
        }
                
        public void Lose()
        {
            _popupShower.SetActiveLosePanel(true);
        }
                
        public List<Cell> GetCells() 
            => Cells;

        private void OnEnable()
        {
            _pool.OnFigureClicked += FigureClicked;
            _pool.OnNumActiveIsZero += NumActiveIsZero;
            resetFieldButton.onClick.AddListener(OnResetButtonClicked);
        }

        private void OnDisable()
        {
            _pool.OnFigureClicked -= FigureClicked;
            _pool.OnNumActiveIsZero -= NumActiveIsZero;
            resetFieldButton.onClick.RemoveListener(OnResetButtonClicked);
        }

        private void NumActiveIsZero()
        {
            Win();
        }

        private void Win()
        {
            _popupShower.SetActiveWinPanel(true);
        }

        private void OnResetButtonClicked()
        {
            OnResetClicked?.Invoke();
        }

        private void FigureClicked(Figure figure)
        {
            OnFigureClicked?.Invoke(figure);            
        }
    }
}
