using UnityEngine;
using UnityEditor;
using Character;

[CustomEditor (typeof(Bot))]
public class VisualDebuging : Editor
{
    private void OnSceneGUI()
    {
        // Окружность
        Bot bot = (Bot)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(bot.transform.position, Vector3.up, bot.transform.forward, 360, bot.viewRadius);

        //Угол общора бота
        Vector3 viewAngleA = bot.DirFromAnglee(-bot.viewAngle / 2, false);
        Vector3 viewAngleB = bot.DirFromAnglee(bot.viewAngle / 2, false);

        Handles.DrawLine(bot.transform.position, bot.transform.position + viewAngleA * bot.viewRadius);
        Handles.DrawLine(bot.transform.position, bot.transform.position + viewAngleB  * bot.viewRadius);

        // Поиск игрока
        Handles.color = Color.red;
        var dirToTarget = (bot.Player.transform.position - bot.transform.position).normalized;
        if (Vector3.Distance(bot.transform.position, bot.Player.transform.position) < bot.viewRadius)
        {
            if (Vector3.Angle(bot.transform.forward, dirToTarget) < bot.viewAngle / 2)
            {
                Handles.DrawLine(bot.transform.position, bot.Player.transform.position);
            }
        }
    }
}
