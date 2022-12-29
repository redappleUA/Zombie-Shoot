using Opsive.UltimateCharacterController.Traits;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static GameObject player;
    private Spawner spawner;

    private static GameOverScreen gameOverScreen;
    private FinishScreen finishScreen;
    private PauseScreen pauseScreen;
    private bool isOpened = false;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = FindObjectOfType<Spawner>();

        gameOverScreen = FindObjectOfType<GameOverScreen>(true);
        finishScreen = FindObjectOfType<FinishScreen>(true);
        pauseScreen = FindObjectOfType<PauseScreen>(true);
    }

    private void FixedUpdate()
    {
        if (!player.GetComponent<Health>().IsAlive() && isOpened == false)
        {
            gameOverScreen.OpenGameOverScreen();
            isOpened = true;
        }
        if (spawner.waves.Count == 0 && isOpened == false)
        {
            finishScreen.OpenFinishScreen();
            isOpened = true;
        }
        if (Input.GetKeyDown(KeyCode.L)) pauseScreen.OpenPauseScreen();
    }
}
