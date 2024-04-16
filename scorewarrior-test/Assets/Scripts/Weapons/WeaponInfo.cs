using UnityEngine;

namespace Scorewarrior.Test.Weapons
{
    [CreateAssetMenu(fileName = "Weapon Info", menuName = "Scriptable Objects/Create Weapon Info")]
	public class WeaponInfo : ScriptableObject
	{
		[field: SerializeField] public BulletPrefab BulletPrefab { get; private set; }
		[field: SerializeField] public float Damage { get; private set; }
		[field: SerializeField] public float Accuracy { get; private set; }
		[field: SerializeField] public float FireRate { get; private set; }
		[field: SerializeField] public uint ClipSize { get; private set; }
		[field: SerializeField] public float ReloadTime { get; private set; }
	}
}