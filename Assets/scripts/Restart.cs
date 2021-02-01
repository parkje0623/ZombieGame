using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private int highestScore;

    public void RestartGame()
    {
        if (GameManage.isHighscore)
        {
            highestScore = GameManage.kills;
            PlayerPrefs.SetInt("HighestKills", highestScore);
            PlayerPrefs.Save();
        }

        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
