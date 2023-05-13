using System.Collections;
using System.Collections.Generic;
using UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public void OnStartGame()
    {
        LoadLvl.StartLoad(E_Scenes.Level1Train);
    }
    public void OnExit()
    {
        Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
