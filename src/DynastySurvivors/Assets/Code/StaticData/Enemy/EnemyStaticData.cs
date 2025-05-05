using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.StaticData.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/EnemyData")]
    public class EnemyStaticData : ScriptableObject
    {
        [FormerlySerializedAs("EnemyTypeId")]
        [BoxGroup("Basic Info"), LabelWidth(120)]
        [GUIColor(0.8f, 1f, 1f)]
        public EnemyTypeId EnemyType;

        [BoxGroup("Basic Info"), PreviewField(60), HideLabel]
        [GUIColor(1f, 1f, 0.8f)]
        public GameObject Prefab;

        [BoxGroup("Basic Info"), LabelWidth(120)]
        [MinValue(1), Tooltip("Initial health of the enemy.")]
        [GUIColor(0.7f, 1f, 0.7f)]
        public float Health = 50f;
        
        [BoxGroup("Basic Info"), LabelWidth(120)]
        [MinValue(1), Tooltip("Initial health of the enemy.")]
        [GUIColor(0.7f, 1f, 0.7f)]
        public int MaxLootValue = 15;
        
        [BoxGroup("Basic Info"), LabelWidth(120)]
        [MinValue(1), Tooltip("Initial health of the enemy.")]
        [GUIColor(0.7f, 1f, 0.7f)]
        public int MinLootValue = 1;

        [BoxGroup("Movement"), LabelWidth(120)]
        [MinValue(0), Tooltip("Speed at which the enemy moves.")]
        [GUIColor(0.8f, 0.9f, 1f)]
        public float MoveSpeed = 1f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0, 10), Tooltip("Cooldown between attacks (in seconds).")]
        [GUIColor(1f, 0.9f, 0.8f)]
        public float AttackCooldown = 3.0f;

        [FormerlySerializedAs("Cleavage")]
        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0.01f, 2), Tooltip("Width of the attack hitbox.")]
        [GUIColor(1f, 0.95f, 0.85f)]
        public float AttackCleavage = 0.1f;        
        
        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0.5f, 2), Tooltip("Width of the attack hitbox.")]
        [GUIColor(1f, 0.95f, 0.85f)]
        public float AttackOffsetY = 1f;        
        
        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0.5f, 2), Tooltip("Width of the attack hitbox.")]
        [GUIColor(1f, 0.95f, 0.85f)]
        public float AttackOffsetForward = 1f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0.1f, 2), Tooltip("Effective distance for attack reach.")]
        [GUIColor(1f, 0.95f, 0.85f)]
        public float EffectiveDistance = 0.5f;

        [BoxGroup("Attack Settings"), LabelWidth(120)]
        [Range(0, 100), Tooltip("How much damage the enemy deals.")]
        [GUIColor(1f, 0.8f, 0.8f)]
        public float Damage = 10f;
    }
}