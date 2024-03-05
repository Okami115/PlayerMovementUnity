using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load leves.
/// </summary>
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 1;


    /// <summary>
    /// Load scene by index.
    /// </summary>
    /// <param name="buildIndex"></param>
    [ContextMenu("Load Level 1")]
    public void LoadLevel(int buildIndex)
    {
        if (buildIndex != -1)
        {
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
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

