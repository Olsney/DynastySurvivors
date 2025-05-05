using System.Linq;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.StaticData;
using Code.StaticData.Level;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Collect Enemy Spawners"))
            {
                var levelData = (LevelStaticData)target;

                levelData.EnemySpawners = 
                    FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None)
                        .Select(x => new EnemySpawnerData(
                            x.GetComponent<UniqueId>().Id,
                            x.EnemyTypeId,
                            x.transform.position))
                        .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;

                EditorUtility.SetDirty(levelData);
            }
        }
    }
}