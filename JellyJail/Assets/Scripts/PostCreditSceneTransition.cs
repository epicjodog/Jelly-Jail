using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostCreditSceneTransition : MonoBehaviour
{
    AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        audioMan = GetComponent<AudioManager>();
        audioMan.Play("Post Credits Music");

        Invoke(nameof(MainMenuTransition), 59);
    }

    void MainMenuTransition()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
