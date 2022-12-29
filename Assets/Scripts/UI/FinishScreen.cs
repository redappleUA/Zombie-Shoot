using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections.Generic;
using Cursor = UnityEngine.Cursor;
using System.Collections;

public class FinishScreen : MonoBehaviour
{
    private Label score;
    private Label highScore;
    private Button nextButton;
    private Button exitButton;

    /// <summary>
    /// Check did the player finish
    /// </summary>
    public static bool isFinished { get; private set; }
    private Score scorePoint;
    private GameOverScreen gameOverScreen;

    private AudioSource clip;

    private void Awake()
    {
        clip = GetComponent<AudioSource>();
        gameObject.SetActive(false);
        scorePoint = FindObjectOfType<Score>();
        gameOverScreen = FindObjectOfType<GameOverScreen>(true);
    }

    private void Start() => isFinished = false;

    public void OpenFinishScreen()
    {
        gameOverScreen.gameObject.SetActive(false);
        gameObject.SetActive(true);
        isFinished = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        var root = GetComponent<UIDocument>().rootVisualElement;

        nextButton = root.Q<Button>("NextButton");
        exitButton = root.Q<Button>("ExitButton");
        score = root.Q<Label>("Score");
        highScore = root.Q<Label>("HighScore");

        nextButton.clicked += NextButtonPressed;
        exitButton.clicked += ExitButtonPressed;

        score.text = "Score: " + scorePoint.ScorePoint.ToString();
        score.style.display = DisplayStyle.Flex;

        scorePoint.SetHighscore();
        highScore.text = "Highscore: " + scorePoint.highscore.ToString();
        highScore.style.display = DisplayStyle.Flex;

        Time.timeScale = 0;
    }

    public void NextButtonPressed()
    {
        clip.Play();
        StartCoroutine(ButtonClip());
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    IEnumerator ButtonClip()
    {
        yield return new WaitUntil(() => !clip.isPlaying);
    }
}
