using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public int level1BuildIndex = 1;

    [ContextMenu("Load Level 1")]
    public void LoadLevel1(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    [ContextMenu("Load Level 1", true)]
    private bool ValidateLoadLevel1()
    {
        return Application.isPlaying;
    }


    [ContextMenu("Unload Level")]

    private void UnloadScene() 
    {
        SceneManager.UnloadSceneAsync(level1BuildIndex);
    }



}

