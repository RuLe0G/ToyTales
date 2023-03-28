using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMP_SceneSwich : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.F3))
            SceneManager.LoadScene(2);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("dontDestr");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}