using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    private Spawner killedCount;
    /// <summary>
    /// Highscore
    /// </summary>
    public string highscore { get; private set; }

    /// <summary>
    /// the Score
    /// </summary>
    public int ScorePoint 
    { 
        get 
        { 
            if(killedCount.killedZombies.Count != 0)
                return killedCount.killedZombies.Count; 
            else return 0;
        } 
    }

    void Start()
    {
        killedCount = FindObjectOfType<Spawner>().GetComponent<Spawner>();

        if (PlayerPrefs.HasKey("Highscore") == true)
        {
            highscore = PlayerPrefs.GetInt("Highscore").ToString();
        }
        else
        {
            highscore = "No High Scores Yet";
        }
    }

    public void SetHighscore()
    {
        if(PlayerPrefs.GetInt("Highscore") < ScorePoint)
        {
            PlayerPrefs.SetInt("Highscore", ScorePoint);
            highscore = PlayerPrefs.GetInt("Highscore").ToString();
        }
    }

    public void ClearHighscores()
    {
        PlayerPrefs.DeleteKey("Highscore");
        highscore = "No High Scores Yet";
    }
}