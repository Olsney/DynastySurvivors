using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Services.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/EnemyData")]
    public class EnemyStaticData : ScriptableObject
    {
        [BoxGroup("Basic Info"), LabelWidth(120)]
        [GUIColor(0.8f, 1f, 1f)]
        public EnemyTypeId EnemyTypeId;

        [BoxGroup("Basic Info"), PreviewField(60), HideLabel]
        [GUIColor(1f, 1f, 0.8f)]
        public GameObject Prefab;

        [BoxGroup("Basic Info"), LabelWidth(120)]
        [MinValue(1), Tooltip("Initial health of the enemy.")]
        [GUIColor(0.7f, 1f, 0.7f)]
        public float Health = 50f;

        [BoxGroup("Movement"), LabelWidth(120)]
        [MinValue(0), Tooltip("Speed at which the enemy moves.")]
        [GUIColor(0.8f, 0.9f, 1f)]
        public float MoveSpeed = 1f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0, 10), Tooltip("Cooldown between attacks (in seconds).")]
        [GUIColor(1f, 0.9f, 0.8f)]
        public float AttackCooldown = 3.0f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0.5f, 2), Tooltip("Width of the attack hitbox.")]
        [GUIColor(1f, 0.95f, 0.85f)]
        public float Cleavage = 0.5f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0.5f, 2), Tooltip("Effective distance for attack reach.")]
        [GUIColor(1f, 0.95f, 0.85f)]
        public float EffectiveDistance = 0.5f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0, 100), Tooltip("How much damage the enemy deals.")]
        [GUIColor(1f, 0.8f, 0.8f)]
        public float Damage = 10f;
    }
}