using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scorewarrior.Runtime.Weapons
{
    [CreateAssetMenu(fileName = "Weapon Info", menuName = "Scriptable Objects/Create Weapon Info")]
	public class WeaponInfo : ScriptableObject
	{
		[field: SerializeField] public BulletPrefab BulletPrefab { get; private set; }
		[field: SerializeField] public List<WeaponStat> Stats { get; private set; }

		public float GetValue(WeaponStatType statType) => Stats.Find(stat => stat.Type == statType).Value;
	}

	[Serializable]
	public struct WeaponStat
	{
		public WeaponStatType Type;
		public float Value;
	}
}