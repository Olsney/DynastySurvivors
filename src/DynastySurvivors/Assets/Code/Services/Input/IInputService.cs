using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}