using UnityEngine;
using Character;

namespace FMS
{
    public class Patrol : MonoBehaviour, IState
    {
        public void Enter(Bot bot)
        {
            bot.PlayerAnimState = AnimState.Walk;
            bot.UpdateSpeed(bot.SpeedPatrulMove);
            bot.FindNearestTarget();
        }

        public void Execute(Bot bot)
        {
            if (!bot.PlayerDetected())
            {
                bot.MoveToNextTarget();
            }
            else
            {
                bot.ChangeState(new Pursuit());
            }
        }

    }
}