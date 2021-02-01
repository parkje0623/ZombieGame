using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject highscoreText;
    [SerializeField] private GameObject quitGame;

    public static int kills;
    public static bool isHighscore;

    private GameObject countKills;
    private bool pause;
    private bool restartGame;

    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        countKills = GameObject.Find("killScore");
        pause = false;
        restartGame = false;
        isHighscore = false;
    }

    // Update is called once per frame
    void Update()
    {
        countKills.GetComponent<Text>().text = "Kill: " + kills;

        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            Time.timeScale = 0;
            pause = true;
            playerController.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            pause = false;
            playerController.GetComponent<FirstPersonController>().enabled = true;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        if (Player.gameover && !restartGame)
        {
            Cursor.lockState = CursorLockMode.None;
            restartGame = true;
            Cursor.visible = true;
            restart.SetActive(true);
            highscoreText.SetActive(true);
            quitGame.SetActive(true);
            playerController.GetComponent<FirstPersonController>().enabled = false;
            
            if (kills > PlayerPrefs.GetInt("HighestKills"))
            {
                isHighscore = true;
                highscoreText.GetComponent<Text>().text = "Most Kills: " + kills.ToString();
            }
            else
            {
                highscoreText.GetComponent<Text>().text = "Most Kills: " + PlayerPrefs.GetInt("HighestKills").ToString();
            }
        }
    }
}
