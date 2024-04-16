using UnityEngine;
using Scorewarrior.Test.Weapons;
using Scorewarrior.Test.Bootstrap;
using Scorewarrior.Test.Characters.States;

namespace Scorewarrior.Test.Characters
{
	public class Character
	{
		public CharacterInfo Info { get; private set; }
		public CharacterPrefab Prefab { get; private set; }
		public Weapon Weapon { get; private set; }
		public Battlefield Battlefield { get; private set; }

		private float _health;
		private float _armor;
		private State _state;
		private Character _currentTarget;
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
			SetStateIdle();
		}

		public void Update(float deltaTime)
		{
			if (IsAlive)
			{
				_state.Update(deltaTime);
			}
			else
			{
				_state = null;
				_currentTarget = null;
			}
		}

		public void SetStateIdle()
		{
			_state = new StateIdle(this);
		}

		public void SetStateAiming(Character target)
		{
			_currentTarget = target;
			_state = new StateAiming(this, target);
		}

		public void SetStateReloading()
		{
			_state = new StateReloading(this, _currentTarget);
		}

		public void SetStateShooting()
		{
			_state = new StateShooting(this, _currentTarget);
		}

		public void TakeDamage(float damage)
		{
			if (_armor > 0)
			{
				_armor -= damage;
			}
			else if (_health > 0)
			{
				_health -= damage;
			}
			if (_armor <= 0 && _health <= 0)
			{
				Prefab.Anim.PlayAnimDeath();
			}
		}
	}
}