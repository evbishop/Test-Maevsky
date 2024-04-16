using UnityEngine;
using Scorewarrior.Runtime.Weapons;

namespace Scorewarrior.Runtime.Characters
{
	public class CharacterPrefab : MonoBehaviour
	{
		[field: SerializeField]
		public CharacterInfo Info { get; private set; }

		[field: SerializeField]
		public CharacterAnim Anim { get; private set; }
		
		[field: SerializeField]
		public WeaponPrefab Weapon { get; private set; }
		
		[SerializeField]
		private Transform _rightPalm;

		private Transform _weaponTransform;

		private void Start()
		{
			_weaponTransform = Weapon.transform;
		}

		private void Update()
		{
			if (_rightPalm != null && _weaponTransform != null)
			{
				_weaponTransform.position = _rightPalm.position;
				_weaponTransform.forward = _rightPalm.up;
			}
		}
	}
}
