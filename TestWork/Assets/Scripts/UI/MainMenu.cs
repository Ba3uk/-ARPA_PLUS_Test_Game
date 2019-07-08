using System.Collections;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private UISetting setting;
    [SerializeField] private Animator anim;


    public void OnStart()
    {
        anim.SetBool("OnStart", true);
        StartCoroutine(DeferredStatrtLevel());
    }

    public void OnExit()
    {
        StartCoroutine(DiferredExit());
        anim.SetBool("OnExit", true);
    }

    public void OnSetting()
    {
        setting.Init();
    }

    public void LoadLevel()
    {
        LevelCOntroller.LoadLevel(Scenes.Level_1);
    }

    public IEnumerator DeferredStatrtLevel()
    {
        yield return new WaitForSeconds(3f);
        LoadLevel();
    }

    public IEnumerator DiferredExit()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
