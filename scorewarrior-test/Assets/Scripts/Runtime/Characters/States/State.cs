using Scorewarrior.Runtime.Weapons;

namespace Scorewarrior.Runtime.Characters.States
{
    public abstract class State
    {
        protected CharacterStates stateMachine;
        protected Character character => stateMachine.Character;
        protected Weapon weapon => character.Weapon;

        public abstract void Update(float deltaTime);

        public State(CharacterStates stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}