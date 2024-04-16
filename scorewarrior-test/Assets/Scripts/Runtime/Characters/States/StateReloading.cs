namespace Scorewarrior.Runtime.Characters.States
{
    public class StateReloading : State
    {
        private float _time;
        private Character _target;

        public StateReloading(CharacterStates stateMachine, Character target) : base(stateMachine)
        {
            _time = weapon.Prefab.Info.GetValue(Weapons.WeaponStatType.ReloadTime);
            _target = target;
        }

        public override void Update(float deltaTime)
        {
            character.Prefab.Anim.PlayAnimReloading(weapon.Prefab.Info.GetValue(Weapons.WeaponStatType.ReloadTime) / 3.3f);

            if (_time > 0)
            {
                _time -= deltaTime;
            }
            else
            {
                if (_target != null && _target.IsAlive)
                {
                    stateMachine.SetStateShooting();
                }
                else
                {
                    stateMachine.SetStateIdle();
                }
                weapon.Reload();
            }
        }
    }
}