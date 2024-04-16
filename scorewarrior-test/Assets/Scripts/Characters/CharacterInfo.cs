using UnityEngine;

namespace Scorewarrior.Test.Characters
{
    [CreateAssetMenu(fileName = "Character Info", menuName = "Scriptable Objects/Create Character Info")]
    public class CharacterInfo : ScriptableObject
    {
        [field: SerializeField] public float Accuracy { get; private set; }
		[field: SerializeField] public float Dexterity { get; private set; }
		[field: SerializeField] public float MaxHealth { get; private set; }
		[field: SerializeField] public float MaxArmor { get; private set; }
		[field: SerializeField] public float AimTime { get; private set; }
    }
}