using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{

    [SerializeField] private EnemyController enemyController;
    [SerializeField] private int tutorialIndex;
    [SerializeField] private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        enemyController.swichScene += swichScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void swichScene()
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
