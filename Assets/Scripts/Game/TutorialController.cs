using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    [SerializeField] private EnemyController enemyController;
    //TODO: TP2 - Remove unused methods/variables/classes
    [SerializeField] private int tutorialIndex;
    [SerializeField] private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        enemyController.swichScene += swichScene;
    }

    //TODO: TP2 - Remove unused methods/variables/classes
    // Update is called once per frame
    void Update()
    {

    }

    private void swichScene()
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
