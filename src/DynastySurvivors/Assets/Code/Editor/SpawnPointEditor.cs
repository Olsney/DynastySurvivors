using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(SpawnPoint))]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        private const float CircleGizmoRadius = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnPoint spawnPoint, GizmoType gizmo)
        {
            CircleGizmo(spawnPoint.transform, CircleGizmoRadius, Color.blue);
        }

        private static void CircleGizmo(Transform transform, float radius, Color color)
        {
            Gizmos.color = color;

            Vector3 position = transform.position;
            
            Gizmos.DrawSphere(position, radius);
        }
    }
}