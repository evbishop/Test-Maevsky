using UnityEngine;

namespace Scorewarrior.Runtime.Characters
{
	public class CharacterAnim : MonoBehaviour
	{
		private readonly int _paramDeath = Animator.StringToHash("die");
		private readonly int _paramAiming = Animator.StringToHash("aiming");
		private readonly int _paramReloading = Animator.StringToHash("reloading");
		private readonly int _paramReloadTime = Animator.StringToHash("reload_time");
		private readonly int _paramShooting = Animator.StringToHash("shoot");

		[SerializeField] private Animator _animator;
		
		public void PlayAnimDeath()
		{
			_animator.SetTrigger(_paramDeath);
		}

		public void PlayAnimIdle()
		{
			_animator.SetBool(_paramAiming, false);
			_animator.SetBool(_paramReloading, false);
		}

		public void PlayAnimAiming()
		{
			_animator.SetBool(_paramAiming, true);
			_animator.SetBool(_paramReloading, false);
		}

		public void PlayAnimShooting()
		{
			_animator.SetTrigger(_paramShooting);
		}

		public void PlayAnimReloading(float reloadTime)
		{
			_animator.SetBool(_paramAiming, true);
			_animator.SetBool(_paramReloading, true);
			_animator.SetFloat(_paramReloadTime, reloadTime);
		}
	}
}
