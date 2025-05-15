using UnityEngine;

namespace Common
{
    public sealed class Cooldown
    {
        private float _current;

        private readonly float _duration;

        public Cooldown(float duration, float current = 0)
        {
            _duration = duration;
            _current = current;
        }

        public bool IsExpired()
        {
            return _current <= 0;
        }

        public float GetProgress()
        {
            return _current / _duration;
        }

        public void Reset()
        {
            _current = _duration;
        }

        public void Tick(float deltaTime)
        {
            _current = Mathf.Max(0, _current - deltaTime);
        }
    }
}
