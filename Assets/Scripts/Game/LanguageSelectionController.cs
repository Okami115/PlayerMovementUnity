using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSelectionController : MonoBehaviour
{

    [SerializeField] int indexScene;
    [SerializeField] int indexLanguageScene;
    void AWake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            Debug.LogError($"No language Selected");
            SceneManager.LoadScene(indexLanguageScene);
        }
        else
        {
            Debug.Log($"{PlayerPrefs.GetString("Language")}");
            SceneManager.LoadScene(indexScene);
        }

    }


}
