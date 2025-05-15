using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FigureData/FigureBoxData")]
public sealed class FigureBoxData : FigureData
{
    public ColliderBoxData Collider;

    [Serializable]
    public struct ColliderBoxData
    {
        public Vector2 Size;
        public ColliderType Type;                
    }
}
