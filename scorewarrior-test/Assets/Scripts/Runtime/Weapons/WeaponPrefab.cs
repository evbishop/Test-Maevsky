using UnityEngine;
using Scorewarrior.Runtime.Characters;

namespace Scorewarrior.Runtime.Weapons
{
	public class WeaponPrefab : MonoBehaviour
	{
		[field: SerializeField] public WeaponInfo Info { get; private set; }
		[field: SerializeField] public Transform BarrelTransform { get; private set; }

		public void Fire(Character character, bool hit)
		{
			BulletPrefab bullet = Instantiate(Info.BulletPrefab);
			bullet.transform.position = BarrelTransform.position;
			bullet.Init(this, character, hit);
		}
	}
}