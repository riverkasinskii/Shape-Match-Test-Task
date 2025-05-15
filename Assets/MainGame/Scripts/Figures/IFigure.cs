using UnityEngine;

namespace SampleGame
{
    public interface IFigure
    {
        public GameObject GetRootGameObject();
        public Transform GetTransform();
        public bool TryGetRigidbody2D(out Rigidbody2D rigidbody2D);        
        public bool GetFirstInstallState();
        public void FirstInstallFigure(bool state);
        public bool GetFigureState();
        public void TryInstallBoxColliderParams(Vector2 size);
        public void TryInstallCircleColliderParams(float radius);
        public void InstallView(Sprite shape, Color color, Sprite animal);
        public void InstallId(int id);        
    }
}
