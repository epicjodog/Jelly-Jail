using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    public Animator barAnim;
    public GameObject[] windowArray;

    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;
        barAnim.SetTrigger("ExitLevel");
        Invoke(nameof(WaitingTransition), 1);
    }
    private void WaitingTransition()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickSettings()
    {
        windowArray[0].SetActive(true);
    }

    public void OnClickItems()
    {
        windowArray[1].SetActive(true);
    }

    public void OnClickBack(int temp)
    {
        windowArray[temp].SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
