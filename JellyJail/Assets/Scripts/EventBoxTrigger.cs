using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// Attached to box trigger to invoke an event when the player enters the trigger
/// </summary>
public class EventBoxTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;
    public UnityEvent onExit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onTrigger.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onExit.Invoke();
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void NextLevel()
    {
        switch (GlobalController.Instance.currentLevel)
        {
            case 1:
                SceneManager.LoadScene("Level2");
                GlobalController.Instance.currentLevel = 2;
                break;
            case 2:
                SceneManager.LoadScene("Level3");
                GlobalController.Instance.currentLevel = 3;
                break;
            case 3:
                SceneManager.LoadScene("Credits");
                GlobalController.Instance.currentLevel = 4;
                break;
        }
    }

    public void DebugLog(string text)
    {
        Debug.Log(text);
    }
}
