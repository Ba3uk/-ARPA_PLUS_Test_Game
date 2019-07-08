using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCOntroller : MonoBehaviour
{
    
    public static void LoadLevel(Scenes id)
    {
        SceneManager.LoadSceneAsync((int)id);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


}

public enum Scenes
{
    Menu,
    Level_1
}
