using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLanguage : MonoBehaviour
{

    [ContextMenu("Set Language")]
    public void setLanguage(string langauge)
    {
        PlayerPrefs.SetString("Language", langauge);
        SceneManager.LoadScene(0);
    }
}
