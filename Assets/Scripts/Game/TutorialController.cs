using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    [SerializeField] private EnemyController enemyController;
    [SerializeField] private int levelIndex;

    void Start()
    {
        enemyController.changeScene += swichScene;
    }
    private void swichScene()
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
