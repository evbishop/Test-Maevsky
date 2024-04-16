namespace Scorewarrior.Test.Characters.States
{
    public class StateReloading : State
    {
        private float _time;
        private Character _target;

        public StateReloading(Character character, Character target) : base(character)
        {
            _time = character.Weapon.Prefab.Info.ReloadTime;
            _target = target;
        }

        public override void Update(float deltaTime)
        {
            character.Prefab.Anim.PlayAnimReloading(weapon.Prefab.Info.ReloadTime / 3.3f);

            if (_time > 0)
            {
                _time -= deltaTime;
            }
            else
            {
                if (_target != null && _target.IsAlive)
                {
                    character.SetStateShooting();
                }
                else
                {
                    character.SetStateIdle();
                }
                weapon.Reload();
            }
        }
    }
}