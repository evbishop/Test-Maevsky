using UnityEngine;
using Scorewarrior.Test.Characters;

namespace Scorewarrior.Test.Weapons
{
	public class WeaponPrefab : MonoBehaviour
	{
		[field: SerializeField] public WeaponInfo Info { get; private set; }
		[field: SerializeField] public Transform BarrelTransform { get; private set; }

		public void Fire(Character character, bool hit)
		{
			BulletPrefab bullet = Instantiate(Info.BulletPrefab, BarrelTransform);
			bullet.transform.position = BarrelTransform.position;
			bullet.Init(this, character, hit);
		}
	}
}