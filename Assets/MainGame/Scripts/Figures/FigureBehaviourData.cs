using UnityEngine;

public enum FigureBehaviourType
{
    Simple,
    Heavy,
    Sticky,
    Explosive,
    Frosen
}

[CreateAssetMenu(fileName = "Behaviour", menuName = "FigureBehaviourData/FigureBehaviour")]
public sealed class FigureBehaviourData : ScriptableObject
{
    public FigureBehaviourType Type;
}
