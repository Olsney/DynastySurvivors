using System.Collections.Generic;
using Code.Logic.EnemySpawners;
using Code.StaticData.Enemy;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(EnemySpawnPoint))]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        private static readonly Color MeshColor =  new Color(0.6f, 1f, 0.3f, 0.6f); 

        private static Dictionary<EnemyTypeId, EnemyStaticData> _cachedEnemyData;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGizmo(EnemySpawnPoint spawnPoint, GizmoType gizmoType)
        {
            if (_cachedEnemyData == null)
            {
                _cachedEnemyData = new Dictionary<EnemyTypeId, EnemyStaticData>();
                var allEnemies = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies");

                foreach (var data in allEnemies)
                {
                    if (!_cachedEnemyData.ContainsKey(data.EnemyType))
                        _cachedEnemyData.Add(data.EnemyType, data);
                }
            }

            if (!_cachedEnemyData.TryGetValue(spawnPoint.EnemyTypeId, out var enemyData) || enemyData.Prefab == null)
                return;

            GameObject prefab = enemyData.Prefab;
            Transform spawnTransform = spawnPoint.transform;

            Gizmos.color = MeshColor;

            foreach (var filter in prefab.GetComponentsInChildren<MeshFilter>())
            {
                if (filter.sharedMesh == null) 
                    continue;

                DrawMesh(filter.sharedMesh, filter.transform, spawnTransform);
            }

            foreach (var skinned in prefab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (!skinned.sharedMesh) 
                    continue;

                Mesh bakedMesh = new Mesh();
                skinned.BakeMesh(bakedMesh);

                DrawMesh(bakedMesh, skinned.transform, spawnTransform);
            }
        }

        private static void DrawMesh(Mesh mesh, Transform part, Transform root)
        {
            Vector3 position = root.TransformPoint(part.localPosition);
            Quaternion rotation = root.rotation * part.localRotation;
            Vector3 scale = Vector3.Scale(root.lossyScale, part.localScale);

            Gizmos.DrawMesh(mesh, 0, position, rotation, scale);
            Gizmos.DrawWireMesh(mesh, 0, position, rotation, scale);
        }
    }
}