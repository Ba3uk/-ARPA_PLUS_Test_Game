using Character;
using UnityEngine;

namespace FMS
{
    public class Fight : IState
    {
        private float _fightRadius = 3.5f;
        private float _lastRadius;
        public void Enter(Bot bot)
        {
            bot.PlayerAnimState = AnimState.Attack;

        }

        public void Execute(Bot bot)
        {
           
        }
    }
}