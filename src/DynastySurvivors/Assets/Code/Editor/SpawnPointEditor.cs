using Code.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(EnemySpawnPoint))]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        private const float CircleGizmoRadius = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawnPoint enemySpawnPoint, GizmoType gizmo)
        {
            CircleGizmo(enemySpawnPoint.transform, CircleGizmoRadius, Color.blue);
        }

        private static void CircleGizmo(Transform spawnPointTransform, float radius, Color color)
        {
            Gizmos.color = color;

            Vector3 position = spawnPointTransform.position;
            
            Gizmos.DrawSphere(position, radius);
        }
    }
}