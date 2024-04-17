using System.Collections.Generic;
using System.Linq;
using Scorewarrior.Runtime.Characters;
using UnityEngine;

namespace Scorewarrior.Runtime.Weapons
{
	public class Weapon
	{
		public WeaponPrefab Prefab { get; private set; }
		public bool IsReady { get; private set; }
		private uint _ammo;
		private float _time;
		public List<WeaponStat> WeaponBonuses { get; } = new();
		public bool HasAmmo => _ammo > 0;

		public Weapon(WeaponPrefab prefab)
		{
			Prefab = prefab;
		}

		public void Setup()
		{
			_ammo = (uint)Mathf.RoundToInt(GetStatValue(WeaponStatType.ClipSize));
		}

		public float GetStatValue(WeaponStatType statType)
		{
			float baseValue = Prefab.Info.GetValue(statType);
			float totalBonusModifier = 1f;
			foreach (var bonus in WeaponBonuses.Where(bonus => bonus.Type == statType))
			{
				totalBonusModifier *= bonus.Value;
			}
			Debug.Log($"{statType} bonus: {totalBonusModifier}. Base value: {baseValue}. Result: {baseValue * totalBonusModifier}");
			return baseValue * totalBonusModifier;
		}

		public void Reload()
		{
			_ammo = (uint)Mathf.RoundToInt(GetStatValue(WeaponStatType.ClipSize));
		}

		public void Fire(Character target, bool hit)
		{
			if (_ammo > 0)
			{
				_ammo -= 1;
				Prefab.Fire(this, target, hit);
				_time = 1.0f / GetStatValue(WeaponStatType.FireRate);
				IsReady = false;
			}
		}

		public void Update(float deltaTime)
		{
			if (!IsReady)
			{
				if (_time > 0)
				{
					_time -= deltaTime;
				}
				else
				{
					IsReady = true;
				}
			}
		}
	}
}