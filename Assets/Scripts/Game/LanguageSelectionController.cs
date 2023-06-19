using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSelectionController : MonoBehaviour
{

    [SerializeField] int indexScene;
    [SerializeField] int indexLanguageScene;
    //TODO: Fix - Wrong name prevents unity message call
    void AWake()
    {
        //TODO: Fix - Make const
        if (!PlayerPrefs.HasKey("Language"))
        {
            Debug.LogError($"No language Selected");
            //TODO: TP2 - SOLID
            SceneManager.LoadScene(indexLanguageScene);
        }
        else
        {
            //TODO: Fix - Bad log/Log out of context
            Debug.Log($"{PlayerPrefs.GetString("Language")}");
            //TODO: TP2 - SOLID
            SceneManager.LoadScene(indexScene);
        }

    }


}
