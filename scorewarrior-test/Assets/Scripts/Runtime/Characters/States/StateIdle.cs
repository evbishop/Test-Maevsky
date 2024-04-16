using Scorewarrior.Runtime.Bootstrap;

namespace Scorewarrior.Runtime.Characters.States
{
    public class StateIdle : State
    {
        public StateIdle(CharacterStates stateMachine) : base(stateMachine)
        {

        }

        public override void Update(float deltaTime)
        {
            character.Prefab.Anim.PlayAnimIdle();

            if (character.Battlefield.TryGetNearestAliveEnemy(character, out Character target))
            {
                stateMachine.SetStateAiming(target);
            }
        }
    }
}