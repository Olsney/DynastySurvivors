using System;

namespace Code.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        float Current { get; }
        float Max { get; }
    }
}