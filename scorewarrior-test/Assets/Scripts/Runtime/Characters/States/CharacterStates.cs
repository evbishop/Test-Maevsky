namespace Scorewarrior.Runtime.Characters.States
{
    public class CharacterStates
    {
        private State _currentState;
        private Character _currentTarget;

        public Character Character { get; private set; }

        public CharacterStates(Character character)
        {
            Character = character;
            SetStateIdle();
        }

        public void Update(float deltaTime)
		{
            _currentState.Update(deltaTime);
		}

        public void SetStateIdle()
		{
			_currentState = new StateIdle(this);
		}

		public void SetStateAiming(Character target)
		{
			_currentTarget = target;
			_currentState = new StateAiming(this, _currentTarget);
		}

		public void SetStateReloading()
		{
			_currentState = new StateReloading(this, _currentTarget);
		}

		public void SetStateShooting()
		{
			_currentState = new StateShooting(this, _currentTarget);
		}
    }
}