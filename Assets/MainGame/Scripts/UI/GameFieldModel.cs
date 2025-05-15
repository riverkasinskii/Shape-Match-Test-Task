using System;
using System.Collections.Generic;
using UnityEngine;

namespace SampleGame
{
    public sealed class GameFieldModel
    {
        public event Action OnLoseGameEvent;

        private const int MATCH_VALUE = 3;        
        
        private readonly Dictionary<int, Cell> _cells = new();
        private readonly Dictionary<int, Dictionary<int, Cell>> _currentBarState = new();        

        public void InstallCells(List<Cell> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                _cells.Add(i, cells[i]);
            }
        }

        public void SetupFigureToEmptyCell((Sprite, Sprite, Color, Sprite) tuple, int id)
        {            
            foreach (var key in _cells.Keys)
            {
                if (_cells[key].IsEmpty)
                {
                    Dictionary<int, Cell> cells = new() { [id] = _cells[key] };
                    if (!_currentBarState.TryAdd(key, cells))
                    {
                        _currentBarState[key].TryAdd(id, _cells[key]);
                    }
                    
                    _cells[key].SetImage(tuple);
                    if (CheckMatch(id))
                    {
                        RemoveCells(id);
                        return;
                    }
                    CheckLoseGame();
                    break;
                }
            }
        }

        private void CheckLoseGame()
        {
            int counter = 0;            
            foreach (var item in _currentBarState.Values)
            {
                if (_currentBarState.Count == _cells.Count && item.Count == 1)
                {
                    counter++;
                    if (counter == _cells.Count)
                    {
                        OnLoseGameEvent?.Invoke();
                    }
                }                
            }                      
        }

        private bool CheckMatch(int currentId)
        {                               
            int currentMatches = 0;            
            
            foreach (var dictionary in _currentBarState.Values)
            {                
                foreach (var key in dictionary.Keys)
                {
                    if (key == currentId && currentMatches < MATCH_VALUE)
                    {                                
                        currentMatches++;
                    }
                    if (currentMatches >= MATCH_VALUE)
                    {
                        return true;
                    }
                }                
            }
            return false;
        }

        private void RemoveCells(int currentId)
        {
            for (int i = 0; i < _currentBarState.Count; i++)
            {
                if (_currentBarState[i].ContainsKey(currentId))
                {
                    _currentBarState[i].TryGetValue(currentId, out Cell cell);
                    cell.ResetState();                    
                    _currentBarState[i].Remove(currentId);
                }
            }            
        }
    }
}