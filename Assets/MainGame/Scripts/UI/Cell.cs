using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class Cell : MonoBehaviour
    {
        public bool IsEmpty { get; private set; } = true;

        [SerializeField]
        private Image _shape;

        [SerializeField]
        private Image _color;

        [SerializeField]
        private Image _animal;

        public void SetImage((Sprite, Sprite, Color, Sprite) tuple)
        {
            SetStateCell(true);

            _shape.sprite = tuple.Item1;
            _color.sprite = tuple.Item2;
            _color.color = tuple.Item3;
            _animal.sprite = tuple.Item4;
        }

        private void SetStateCell(bool state)
        {
            IsEmpty = !state;
            _shape.gameObject.SetActive(state);
            _color.gameObject.SetActive(state);
            _animal.gameObject.SetActive(state);
        }

        public void ResetState()
        {
            SetStateCell(false);
        }
    }
}
