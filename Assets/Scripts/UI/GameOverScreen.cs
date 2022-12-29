using Opsive.UltimateCharacterController.Traits;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameOverScreen : MonoBehaviour
{
    private Label score;
    private Button restartButton;
    private Button exitButton;

    private bool isClicked = false;
    private static Score scorePoint;

    private void Awake()
    {
        gameObject.SetActive(false);
        scorePoint = FindObjectOfType<Score>();
    }

    void RestartButtonPressed()
    {
        if (!isClicked) //It clicked many times cause event system is bugging
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");

            isClicked = true;
        }
    }

    public static void ExitButtonPressed() => Application.Quit();
    public void OpenGameOverScreen()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        var root = GetComponent<UIDocument>().rootVisualElement;

        restartButton = root.Q<Button>("RestartButton");
        exitButton = root.Q<Button>("ExitButton");
        score = root.Q<Label>("Score");

        restartButton.clicked += RestartButtonPressed;
        exitButton.clicked += ExitButtonPressed;

        score.text = "Score: " + scorePoint.ScorePoint.ToString();
        score.style.display = DisplayStyle.Flex;
    }
}