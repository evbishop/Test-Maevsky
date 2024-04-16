using UnityEngine;
using Scorewarrior.Runtime.Characters;

namespace Scorewarrior.Runtime.Weapons
{
	public class BulletPrefab : MonoBehaviour
	{
		[SerializeField] private BulletInfo _bulletInfo;

		private Character _target;
		private WeaponPrefab _weapon;
		private bool _isHit;
		private Vector3 _startPosition;
		private Vector3 _direction;
		private float _totalDistance;
		private float _currentDistance;
		private Transform _transform;

		private void Awake()
		{
			_transform = transform;
		}

		public void Init(WeaponPrefab weapon, Character target, bool isHit)
		{
			_weapon = weapon;
			_target = target;
			_isHit = isHit;
			_startPosition = _transform.position;
			_currentDistance = 0;

			Vector3 targetPosition = target.Position + Vector3.up * 2.0f;
			_direction = Vector3.Normalize(targetPosition - _startPosition);
			_totalDistance = Vector3.Distance(targetPosition, _startPosition);
		}

		public void Update()
		{
			_currentDistance += Time.deltaTime * _bulletInfo.Speed;
			if (_currentDistance < _totalDistance)
			{
				_transform.position = _startPosition + _currentDistance * _direction;
			}
			else
			{
				if (_isHit)
				{
					_target.TakeDamage(_weapon.Info.Damage);
				}
				Destroy(gameObject);
			}
		}
	}
}