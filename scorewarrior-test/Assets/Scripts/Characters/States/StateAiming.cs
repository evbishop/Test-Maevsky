namespace Scorewarrior.Test.Characters.States
{
    public class StateAiming : State
    {
        private float _time;
        private Character _target;

        public StateAiming(Character character, Character target) : base(character)
        {
            _time = character.Info.AimTime;
            _target = target;
            character.Transform.LookAt(target.Position);
        }

        public override void Update(float deltaTime)
        {
            character.Prefab.Anim.PlayAnimAiming();

            if (_target != null && _target.IsAlive)
            {
                if (_time > 0)
                {
                    _time -= deltaTime;
                }
                else
                {
                    character.SetStateShooting();
                }
            }
            else
            {
                character.SetStateIdle();
            }
        }
    }
}