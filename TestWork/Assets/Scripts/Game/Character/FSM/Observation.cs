using Character;
using UnityEngine;

namespace FMS
{
    public class Observation : IState
    {
        private float localTime;
        private float _angle;
        private float _distance;

        public void Enter(Bot bot)
        {
            bot.PlayerAnimState = AnimState.Find;
            localTime = bot.TimeToSearchPlayer;
            _angle = bot.viewAngle;
            _distance = bot.viewRadius;

            Debug.Log("123");
        }

        public void Execute(Bot bot)
        {
            if (localTime > 0)
            {
                localTime -= Time.deltaTime;

                bot.viewRadius = Mathf.Lerp(bot.viewRadius, _distance * 1.5f, bot.TimeToSearchPlayer * Time.deltaTime);
                bot.viewAngle = Mathf.Lerp(bot.viewAngle, 360, bot.TimeToSearchPlayer * Time.deltaTime / 2);

                if (bot.PlayerDetected())
                {
                    bot.UpdateLastTargetPos();
                    ResetBotStats(bot);
                    bot.ChangeState(new Pursuit());
                }
            }
            else
            {
                ResetBotStats(bot);
                bot.ChangeState(new Patrol());
            }
        }

        private void ResetBotStats(Bot bot)
        {
            bot.viewAngle = _angle;
            bot.viewRadius = _distance;
        }
    }
}