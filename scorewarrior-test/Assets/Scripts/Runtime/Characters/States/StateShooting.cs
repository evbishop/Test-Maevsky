using UnityEngine;

namespace Scorewarrior.Runtime.Characters.States
{
    public class StateShooting : State
    {
        private Character _target;

        public StateShooting(CharacterStates stateMachine, Character target) : base(stateMachine)
        {
            _target = target;
        }

        public override void Update(float deltaTime)
        {
            character.Prefab.Anim.PlayAnimAiming();

            if (_target != null && _target.IsAlive)
            {
                if (weapon.HasAmmo)
                {
                    if (weapon.IsReady)
                    {
                        float random = Random.Range(0.0f, 1.0f);
                        bool hit = random <= character.Info.Accuracy &&
                            random <= weapon.Prefab.Info.Accuracy &&
                            random >= _target.Info.Dexterity;
                        weapon.Fire(_target, hit);
                        
                        character.Prefab.Anim.PlayAnimShooting();
                    }
                    else
                    {
                        weapon.Update(deltaTime);
                    }
                }
                else
                {
                    stateMachine.SetStateReloading();
                }
            }
            else
            {
                stateMachine.SetStateIdle();
            }
        }
    }
}