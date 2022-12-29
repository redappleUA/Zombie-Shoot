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
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = FindObjectOfType<Spawner>();

        gameOverScreen = FindObjectOfType<GameOverScreen>();
        finishScreen = FindObjectOfType<FinishScreen>();
        pauseScreen = FindObjectOfType<PauseScreen>();
    }

    private void FixedUpdate()
    {
        if (!player.GetComponent<Health>().IsAlive()) 
            gameOverScreen.OpenGameOverScreen();
        if (spawner.waves.Count == 0) finishScreen.OpenFinishScreen();
        if (Input.GetKeyDown(KeyCode.Tilde)) { pauseScreen.OpenPauseScreen(); }
    }
}
