using UnityEngine;

namespace SampleGame
{
    public sealed class FigureBehaviourController
    {        
        private const int COEFFFICIENT = 2;

        public void InstallParams(IFigure source, FigureBehaviourData data)
        {
            if (source.GetFirstInstallState())
                return;            

            if (data.Type == FigureBehaviourType.Heavy && source.TryGetRigidbody2D(out Rigidbody2D rigidbody2D))
            {
                rigidbody2D.gravityScale *= COEFFFICIENT;                
            }
            if (data.Type == FigureBehaviourType.Sticky)
            {
                //TODO
            }
            if (data.Type == FigureBehaviourType.Explosive)
            {
                //TODO
            }
            if (data.Type == FigureBehaviourType.Frosen)
            {
                //TODO
            }
        }
    }
}

