using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLanguage : MonoBehaviour
{

    //TODO: Fix - Context menu doesn't work with parameters
    [ContextMenu("Set Language")]
    public void setLanguage(string langauge)
    {
        PlayerPrefs.SetString("Language", langauge);
        SceneManager.LoadScene(0);
    }
}
