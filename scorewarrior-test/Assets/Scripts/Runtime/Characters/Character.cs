using UnityEngine;
using Scorewarrior.Runtime.Weapons;
using Scorewarrior.Runtime.Bootstrap;
using Scorewarrior.Runtime.Characters.States;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

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
			_health = prefab.Info.GetValue(CharacterStatType.MaxHealth);
			_armor = prefab.Info.GetValue(CharacterStatType.MaxArmor);
			_states = new CharacterStates(this);
		}

		public void GenerateBonuses()
		{
			int characterStatsQuantity = Enum.GetNames(typeof(CharacterStatType)).Length - 1;
			int weaponStatsQuantity = Enum.GetNames(typeof(WeaponStatType)).Length - 1;
			int totalStatsQuantity = characterStatsQuantity + weaponStatsQuantity;

			for (int i = 0; i < 3; i++)
			{
				int r = Random.Range(1, totalStatsQuantity);
				if (r <= characterStatsQuantity)
				{
					CharacterStatType stat = (CharacterStatType)r;
					CharacterBonusesInfo.Instance.GetRandomBonusValue(stat);
				}
				else
				{
					r -= characterStatsQuantity;
					WeaponStatType stat = (WeaponStatType)r;
					WeaponBonusesInfo.Instance.GetRandomBonusValue(stat);
				}
			}
			for (int i = 0; i < 2; i++)
			{

			}
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
				Prefab.ArmorDisplay.Setup(_armor/Prefab.Info.GetValue(CharacterStatType.MaxArmor));
			}
			else if (_health > 0)
			{
				Prefab.ArmorDisplay.gameObject.SetActive(false);
				_health -= damage;
				Prefab.HealthDisplay.Setup(_health/Prefab.Info.GetValue(CharacterStatType.MaxHealth));
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