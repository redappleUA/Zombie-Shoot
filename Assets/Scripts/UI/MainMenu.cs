using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource clip;
    [SerializeField] AudioSource audioSource;

    private Button playButton;
    private Button exitButton;
    private Button musicButton;

    private void Start()
    {
        audioSource.Play();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;

        var root = GetComponent<UIDocument>().rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
        musicButton = root.Q<Button>("MusicButton");
        exitButton = root.Q<Button>("ExitButton");

        playButton.clicked += PlayButtonClicked;
        musicButton.clicked += MusicButtonPressed;
        exitButton.clicked += ExitButtonPressed;

        Time.timeScale = 0;
    }

    void PlayButtonClicked()
    {
        clip.Play();
        StartCoroutine(ButtonClip());
        Cursor.visible = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    void MusicButtonPressed()
    {
        clip.Play();

        if (audioSource.isPlaying)
            audioSource.Stop();
        else audioSource.Play();
    }

    void ExitButtonPressed() 
    {
        clip.Play();
        StartCoroutine(ButtonClip());
        Application.Quit();
    }

    IEnumerator ButtonClip()
    {
        yield return new WaitUntil(() => !clip.isPlaying);
    }
}
