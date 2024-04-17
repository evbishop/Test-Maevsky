using UnityEngine;
using Scorewarrior.Runtime.Weapons;
using Scorewarrior.Runtime.Bootstrap;
using Scorewarrior.Runtime.Characters.States;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

namespace Scorewarrior.Runtime.Characters
{
	public class Character
	{
		private CharacterStates _states;
		private float _health;
		private float _armor;
		private List<CharacterStat> _characterBonuses = new();

		public CharacterPrefab Prefab { get; private set; }
		public Weapon Weapon { get; private set; }
		public Battlefield Battlefield { get; private set; }
		public Transform Transform { get; private set; }

		public Vector3 Position => Transform.position;
		public bool IsAlive => _health > 0 || _armor > 0;

		public event Action<Character> OnCharacterDeath;

		public Character(CharacterPrefab prefab, Weapon weapon, Battlefield battlefield)
		{
			Prefab = prefab;
			Transform = prefab.transform;
			Weapon = weapon;
			Battlefield = battlefield;
			_states = new CharacterStates(this);
			
			GenerateBonuses();
			Weapon.Setup();
			_health = GetStatValue(CharacterStatType.MaxHealth);
			_armor = GetStatValue(CharacterStatType.MaxArmor);
		}

		private void GenerateBonuses()
		{
			int characterStatsQuantity = Enum.GetNames(typeof(CharacterStatType)).Length;
			int weaponStatsQuantity = Enum.GetNames(typeof(WeaponStatType)).Length;
			int totalStatsQuantity = characterStatsQuantity + weaponStatsQuantity;

			for (int i = 0; i < CharacterBonusesInfo.Instance.RollsQuantity; i++)
			{
				int r = Random.Range(1, totalStatsQuantity - 1);
				if (r < characterStatsQuantity)
				{
					CharacterStatType stat = (CharacterStatType)r;
					_characterBonuses.Add(CharacterBonusesInfo.Instance.GetRandomBonusValue(stat));
				}
				else
				{
					r -= characterStatsQuantity;
					r++; // move the range to the right by one position, so we don't include 0, which is "None" in the enum
					WeaponStatType stat = (WeaponStatType)r;
					Weapon.WeaponBonuses.Add(WeaponBonusesInfo.Instance.GetRandomBonusValue(stat));
				}
			}
			for (int i = 0; i < WeaponBonusesInfo.Instance.RollsQuantity; i++)
			{
				int r = Random.Range(1, weaponStatsQuantity);
				WeaponStatType stat = (WeaponStatType)r;
				Weapon.WeaponBonuses.Add(WeaponBonusesInfo.Instance.GetRandomBonusValue(stat));
			}
		}

		public float GetStatValue(CharacterStatType statType)
		{
			float baseValue = Prefab.Info.GetValue(statType);
			float totalBonusModifier = 1f;
			foreach (var bonus in _characterBonuses.Where(bonus => bonus.Type == statType))
			{
				totalBonusModifier *= bonus.Value;
			}
			return baseValue * totalBonusModifier;
		}

		public void Update(float deltaTime)
		{
			if (IsAlive)
			{
				_states.Update(deltaTime);
			}
			else
			{
				_states = null;
			}
		}

		public void TakeDamage(float damage)
		{
			if (_armor > 0)
			{
				_armor -= damage;
				Prefab.ArmorDisplay.Setup(_armor/GetStatValue(CharacterStatType.MaxArmor));
			}
			else if (_health > 0)
			{
				Prefab.ArmorDisplay.gameObject.SetActive(false);
				_health -= damage;
				Prefab.HealthDisplay.Setup(_health/GetStatValue(CharacterStatType.MaxHealth));
			}
			if (_armor <= 0 && _health <= 0)
			{
				Prefab.ArmorDisplay.gameObject.SetActive(false);
				Prefab.HealthDisplay.gameObject.SetActive(false);
				Die();
			}
		}

		private void Die()
		{
			Prefab.Anim.PlayAnimDeath();
			OnCharacterDeath?.Invoke(this);
		}
	}
}