using Scorewarrior.Test.Characters;

namespace Scorewarrior.Test.Weapons
{
	public class Weapon
	{
		public WeaponPrefab Prefab { get; private set; }
		public bool IsReady { get; private set; }
		private uint _ammo;
		private float _time;

		public Weapon(WeaponPrefab prefab)
		{
			Prefab = prefab;
			_ammo = prefab.Info.ClipSize;
		}

		public bool HasAmmo => _ammo > 0;

		public void Reload()
		{
			_ammo = Prefab.Info.ClipSize;
		}

		public void Fire(Character character, bool hit)
		{
			if (_ammo > 0)
			{
				_ammo -= 1;
				Prefab.Fire(character, hit);
				_time = 1.0f / Prefab.Info.FireRate;
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