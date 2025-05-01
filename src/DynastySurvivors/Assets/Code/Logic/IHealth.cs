using System;

namespace Code.Logic
{
    public interface IHealth
    {
        event Action Changed;
        float Current { get; }
        float Max { get; }
        void Initialize(float current, float max);
    }
}