using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Set the language by a button
/// </summary>
public class SetLanguage : MonoBehaviour
{

    /// <summary>
    /// Set the language by a button
    /// </summary>
    [ContextMenu("Set Language")]
    public void setLanguage(string langauge)
    {
        PlayerPrefs.SetString("Language", langauge);
        SceneManager.LoadScene(0);
    }
}
