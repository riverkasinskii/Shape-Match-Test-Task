using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SampleGame
{
    public sealed class Figure : MonoBehaviour, IFigure, IPointerClickHandler
    {
        private bool _figureInstalled = false;

        public event Action<Figure> OnFigureClicked;
        public event Action<Figure> OnDispose;

        [SerializeField] private SpriteRenderer _shape;
        [SerializeField] private SpriteRenderer _color;
        [SerializeField] private SpriteRenderer _animal;
        [SerializeField] private Transform _rootTransformCollider2D;

        private int _id;        
        private CircleCollider2D _circleCollider2D;
        private BoxCollider2D _boxCollider2D;        

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            gameObject.transform.SetPositionAndRotation(position, rotation);
        }

        public void TryInstallBoxColliderParams(Vector2 size)
        {
            if (!_rootTransformCollider2D.TryGetComponent(out Collider2D _))
            {
                _boxCollider2D = _rootTransformCollider2D.AddComponent<BoxCollider2D>();
                _boxCollider2D.size = size;
            }
        }

        public void TryInstallCircleColliderParams(float radius)
        {
            if (!_rootTransformCollider2D.TryGetComponent(out Collider2D _))
            {
                _circleCollider2D = _rootTransformCollider2D.AddComponent<CircleCollider2D>();
                _circleCollider2D.radius = radius;
            }
        }

        public void InstallView(Sprite shape, Color color, Sprite animal)
        {
            _shape.sprite = shape;
            _color.sprite = shape;
            _color.color = color;
            _animal.sprite = animal;            
        }

        public void InstallId(int id)
        {
            _id = id;
        }

        public (Sprite, Sprite, Color, Sprite) GetView()
            => (_shape.sprite, _shape.sprite, _color.color, _animal.sprite);

        public int GetId() => _id;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnFigureClicked?.Invoke(this);
            OnDispose?.Invoke(this);
            gameObject.SetActive(false);
        }

        public bool GetFigureState()
        {
            return gameObject.activeSelf;
        }

        public void FirstInstallFigure(bool state)
        {
            _figureInstalled = state;
        }

        public bool GetFirstInstallState() =>
            _figureInstalled;

        public bool TryGetRigidbody2D(out Rigidbody2D rigidbody2D)
        {
            if (TryGetComponent(out Rigidbody2D rigidbody))
            {
                rigidbody2D = rigidbody;
                return true;
            }
            rigidbody2D = null;
            return false;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public GameObject GetRootGameObject()
        {
            return gameObject;
        }           
    }
}
