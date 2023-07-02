using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageSelectionController : MonoBehaviour
{
    private const string Key = "Language";
    [SerializeField] private int indexScene;
    [SerializeField] private int indexLanguageScene;
    //TODO: Fix - Wrong name prevents unity message call
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(Key))
        {
            //TODO: TP2 - SOLID
            SceneManager.LoadScene(indexLanguageScene);
        }
        else
        {
            //TODO: TP2 - SOLID
            SceneManager.LoadScene(indexScene);
        }

    }


}
