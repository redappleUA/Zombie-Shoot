using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Cursor = UnityEngine.Cursor;

public class PauseScreen : MonoBehaviour
{
    private Button resumeButton;
    private Button restartButton;
    private Button exitButton;

    private AudioSource clip;

    private void Awake()
    {
        clip = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    public void OpenPauseScreen()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        var root = GetComponent<UIDocument>().rootVisualElement;

        restartButton = root.Q<Button>("RestartButton");
        resumeButton = root.Q<Button>("PauseButton");
        exitButton = root.Q<Button>("ExitButton");

        resumeButton.clicked += ResumeButtonClicked;
        restartButton.clicked += RestartButtonPressed;
        exitButton.clicked += ExitButtonPressed;

        Time.timeScale = 0;
    }

    void ResumeButtonClicked()
    {
        clip.Play();
        StartCoroutine(ButtonClip());
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Cursor.visible = false;
    }

    void RestartButtonPressed()
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
