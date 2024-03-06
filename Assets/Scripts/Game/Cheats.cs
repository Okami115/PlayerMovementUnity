using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{

    [SerializeField] private PlayerController Controller;
    [SerializeField] private LevelLoader LevelLoader;
    [SerializeField] private Player player;
    [SerializeField] private float MultiplierFlash;
    [SerializeField] private PlayerMovement Movement;
    [SerializeField] private EnemyController EnemyController;

    private void OnEnable()
    {
        Controller.NextLevel += NextLevelCheat;
        Controller.GodMode += GodModeCheat;
        Controller.Flash += FlashCheat;
        Controller.Nuke += NukeCheat;
    }

    private void OnDisable()
    {
        Controller.NextLevel -= NextLevelCheat;
        Controller.GodMode -= GodModeCheat;
        Controller.Flash -= FlashCheat;
        Controller.Nuke -= NukeCheat;
    }

    private void NextLevelCheat()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        currentScene++;

        if(currentScene >= SceneManager.sceneCountInBuildSettings) 
        {
            currentScene = 0;
        }

        LevelLoader.LoadLevel(currentScene);
    }

    private void GodModeCheat()
    {
        player.GodMode = !player.GodMode;
    }

    private void FlashCheat()
    {
        if(Movement.SpeedMultiplier > Movement.SpeedConstantMultiplier)
        {
            Movement.SpeedMultiplier = Movement.SpeedConstantMultiplier;
        }
        else
        {
            Movement.SpeedMultiplier = Movement.SpeedConstantMultiplier * MultiplierFlash;
        }
    }

    private void NukeCheat()
    {
        EnemyController.Nuke();
    }
}
