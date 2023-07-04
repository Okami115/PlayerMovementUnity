using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Set the language of the game.
/// </summary>
public class LanguageSelectionController : MonoBehaviour
{
    private const string Key = "Language";
    [SerializeField] private int indexScene;
    [SerializeField] private int indexLanguageScene;

    [SerializeField] private LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = new LevelLoader();
        if (!PlayerPrefs.HasKey(Key))
        {
            levelLoader.LoadLevel1(indexLanguageScene);
        }
        else
        {
            levelLoader.LoadLevel1(indexScene);
        }
    }


}
