using UnityEngine;

public enum ColliderType
{
    Box,
    Circle
}

public class FigureData : ScriptableObject
{
    public Sprite Shape;
    public Sprite ShapeForCanvas;
    public Color Color;
    public Sprite Animal;
    public int id;
    public FigureBehaviourData BehaviourData;
}
