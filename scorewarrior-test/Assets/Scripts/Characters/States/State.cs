using Scorewarrior.Test.Weapons;

namespace Scorewarrior.Test.Characters.States
{
    public abstract class State
    {
        protected Character character;
        protected Weapon weapon;

        public abstract void Update(float deltaTime);

        public State(Character character)
        {
            this.character = character;
            weapon = character.Weapon;
        }
    }
}