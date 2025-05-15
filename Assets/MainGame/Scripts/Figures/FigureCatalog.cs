using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FigureCatalog", menuName = "FigureData/FigureCatalog")]
public sealed class FigureCatalog : ScriptableObject, IReadOnlyList<FigureData>
{
    public int Count => _figures.Length;

    [SerializeField]
    private FigureData[] _figures;

    public FigureData this[int index]
    {
        get { return _figures[index]; }
    }

    public FigureData GetFigure(int index)
    {
        return _figures[index];
    }

    public IEnumerator<FigureData> GetEnumerator()
    {
        for (int i = 0, count = _figures.Length; i < count; i++)
            yield return _figures[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
