using UnityEngine;

namespace Code.Logic
{
    public static class PhysicsDebugHelpers
    {
        public static void DrawRaysFromPoint(Vector3 worldPosition, float rayLength, Color color, float seconds)
        {
            Debug.DrawRay(worldPosition, rayLength * Vector3.up, color, seconds);
            Debug.DrawRay(worldPosition, rayLength * Vector3.down, color, seconds);
            Debug.DrawRay(worldPosition, rayLength * Vector3.left, color, seconds);
            Debug.DrawRay(worldPosition, rayLength * Vector3.right, color, seconds);
            Debug.DrawRay(worldPosition, rayLength * Vector3.forward, color, seconds);
            Debug.DrawRay(worldPosition, rayLength * Vector3.back, color, seconds);
        }
    }
}