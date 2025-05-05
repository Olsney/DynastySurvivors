using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.StaticData.Hero
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/HeroData")]
    [InlineEditor]
    [Title("Hero Static Data", subtitle: "Base configuration for a hero", titleAlignment: TitleAlignments.Centered, bold: true)]
    public class HeroStaticData : ScriptableObject
    {
        [FoldoutGroup("General", expanded: true)]
        [GUIColor(0.8f, 0.9f, 1f)]
        [LabelText("Hero Type")]
        [Tooltip("Unique ID of the hero")]
        public HeroTypeId HeroType = HeroTypeId.Woman;


        [FoldoutGroup("General")]
        [GUIColor(0.8f, 1f, 0.8f)]
        [LabelText("Hero Prefab")]
        [Tooltip("Prefab of the hero GameObject")]
        [PreviewField(60, ObjectFieldAlignment.Center)]
        [HideLabel]
        public GameObject Prefab;

        [TabGroup("Combat", "Attack")]
        [GUIColor(1f, 0.7f, 0.7f)]
        [Range(1f, 100f)]
        [LabelText("Damage")]
        [Tooltip("Base damage dealt by the hero")]
        public float Damage = 15f;

        [TabGroup("Combat", "Attack")]
        [GUIColor(1f, 0.8f, 0.8f)]
        [Range(1f, 5f)]
        [LabelText("Damage Radius")]
        [Tooltip("Radius within which the damage is applied")]
        public float DamageRadius = 1.5f;

        [TabGroup("Combat", "Attack")]
        [GUIColor(1f, 1f, 0.6f)]
        [Range(1f, 5f)]
        [LabelText("Attack Cooldown")]
        [Tooltip("Time interval between hero attacks")]
        public float AttackCooldown = 1.5f;

        [TabGroup("Life", "Health")]
        [GUIColor(0.7f, 1f, 1f)]
        [Range(1f, 100f)]
        [LabelText("Current HP")]
        [Tooltip("Current health of the hero at start")]
        public float CurrentHealth = 100;

        [TabGroup("Life", "Health")]
        [GUIColor(0.5f, 1f, 1f)]
        [Range(1f, 100f)]
        [LabelText("Max HP")]
        [Tooltip("Maximum health the hero can have")]
        public float MaxHealth = 100;

        [TabGroup("Move", "Movement")]
        [GUIColor(0.6f, 1f, 0.6f)]
        [Range(1f, 10f)]
        [LabelText("Movement Speed")]
        [Tooltip("Movement speed of the hero")]
        public float MovementSpeed = 3.5f;
    }
}