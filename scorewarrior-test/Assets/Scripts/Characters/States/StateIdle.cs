using Scorewarrior.Test.Bootstrap;

namespace Scorewarrior.Test.Characters.States
{
    public class StateIdle : State
    {
        public StateIdle(Character character) : base(character)
        {

        }

        public override void Update(float deltaTime)
        {
            character.Prefab.Anim.PlayAnimIdle();

            if (character.Battlefield.TryGetNearestAliveEnemy(character, out Character target))
            {
                character.SetStateAiming(target);
            }
        }
    }
}