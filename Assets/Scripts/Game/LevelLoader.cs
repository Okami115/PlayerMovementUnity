using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;

    [ContextMenu("Load Level 1")]
    public void LoadLevel1(int buildIndex)
    {
        Time.timeScale = 1f;
        if (buildIndex != -1)
        {

            SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
        }
        else
        {
           //exit aplication
        }
    }

    [ContextMenu("Load Level 1", true)]
    private bool ValidateLoadLevel1()
    {
        return Application.isPlaying;
    }


    [ContextMenu("Unload Level")]

    private void UnloadScene() 
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

}

