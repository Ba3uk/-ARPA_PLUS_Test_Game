using Character;
using UnityEngine;

namespace FMS
{
    public class Pursuit : IState
    {
        public void Enter(Bot bot)
        {
            bot.PlayerAnimState = AnimState.Run;
            bot.UpdateSpeed(bot.SpeedPursuitMove);
            bot.UpdateLastTargetPos();
        }

        public void Execute(Bot bot)
        {
            bot.MoveToTarget(bot.LastTargetPos);


            if (bot.PlayerDetected())
            {
                bot.UpdateLastTargetPos();

                if (  (Vector3.Distance(bot.transform.position, bot.Player.transform.position) < 2))
                {
                    bot.ChangeState(new Fight());
                }
            }
            else
            {
                if (Vector3.Distance(bot.transform.position, bot.LastTargetPos) < 1)
                {
                    bot.ChangeState(new Observation());
                }

            }
        }
    }
}