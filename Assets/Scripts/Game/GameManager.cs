
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/// <summary>
/// Control the cradits and manage the scenes
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string sceneNameMenu = "Menu";
    [SerializeField] private string sceneNameTutorial = "Tutorial";
    [SerializeField] private string sceneNameLevel1 = "Level 1";

    [SerializeField] private EnemyController enemyController;

    [SerializeField] private int credits;

    public int Credits { get => credits; set => credits = value; }

    private void OnEnable()
    {
        enemyController.endRound += HandleAllEnemiesAreDefeated;
    }

    private void OnDisable()
    {
        enemyController.endRound -= HandleAllEnemiesAreDefeated;
    }

    /// <summary>
    /// When the tutorial enemies are defeated, start level 1
    /// </summary>
    private void HandleAllEnemiesAreDefeated(int currentRound)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneNameTutorial))
        {
            SceneManager.LoadScene(sceneNameLevel1, LoadSceneMode.Single);
        }
    }
}
