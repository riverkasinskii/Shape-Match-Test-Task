using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FigureData/FigureCircleData")]
public sealed class FigureCircleData : FigureData
{
    public ColliderCircleData Collider;

    [Serializable]
    public struct ColliderCircleData
    {
        public float Radius;
        public ColliderType Type;
    }
}
