using UnityEngine;

namespace SampleGame
{
    public interface IFigureSpawner
    {
        int GetNumActiveFigures();
        IFigure Spawn(Vector3 position, Quaternion rotation);
        void Despawn(Figure figure);
    }
}
