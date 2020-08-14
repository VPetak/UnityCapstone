using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    public Text ScoreBoardText;
    public Text inputText;
    public static string userName = "Guest";

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None; //without this, there will be no cursor when returning to the start menu
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuScoreBoard()
    {
        ScoreBoardText.text = File.ReadAllText(@"saves\\testIO.txt");
    }

    public void AdvanceScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetUserName()
    {
        userName = inputText.text;
    }

    public string GetUserName()
    {
        return userName;
    }

    public void QuitGame()
    {
        Application.Quit();
        print("quit");
    }

}
