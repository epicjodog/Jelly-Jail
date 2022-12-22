using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public void Retry()
    {
        switch (GlobalController.Instance.currentLevel)
        {
            case 1:
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                SceneManager.LoadScene("Level3");
                break;
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
