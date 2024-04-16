using UnityEngine;
using Scorewarrior.Runtime.Weapons;
using Scorewarrior.Runtime.Bootstrap;
using Scorewarrior.Runtime.Characters.States;
using System;

namespace Scorewarrior.Runtime.Characters
{
	public class Character
	{
		public event Action<Character> OnCharacterDeath;

		private CharacterStates _states;
		private float _health;
		private float _armor;

		public CharacterInfo Info { get; private set; }
		public CharacterPrefab Prefab { get; private set; }
		public Weapon Weapon { get; private set; }
		public Battlefield Battlefield { get; private set; }
		public Transform Transform { get; private set; }

		public Vector3 Position => Transform.position;
		public bool IsAlive => _health > 0 || _armor > 0;

		public Character(CharacterPrefab prefab, Weapon weapon, Battlefield battlefield, CharacterInfo info)
		{
			Prefab = prefab;
			Transform = prefab.transform;
			Weapon = weapon;
			Battlefield = battlefield;
			Info = info;
			_health = info.MaxHealth;
			_armor = info.MaxArmor;
			_states = new CharacterStates(this);
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
				Prefab.ArmorDisplay.Setup(_armor/Info.MaxArmor);
			}
			else if (_health > 0)
			{
				Prefab.ArmorDisplay.gameObject.SetActive(false);
				_health -= damage;
				Prefab.HealthDisplay.Setup(_health/Info.MaxHealth);
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