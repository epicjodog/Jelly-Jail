using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostCreditSceneTransition : MonoBehaviour
{
    public Animator barAnimator;
    public Animator congratsAnimator;
    public Animator descAnimator;
    public GameObject nextButton;
    public Image winImage;
    AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        audioMan = GetComponent<AudioManager>();
        audioMan.Play("Post Credits Music");
        Invoke(nameof(CreditsAnimations), 1);
        Invoke(nameof(CreditsObject), 2.5f);

        // Invoke(nameof(MainMenuTransition), 59);
    }

    // void MainMenuTransition()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }

    void CreditsAnimations()
    {
        congratsAnimator.SetTrigger("TriggerWin");
        descAnimator.SetTrigger("BeginDesc");
    }
    void CreditsObject()
    {
        nextButton.SetActive(true);
        winImage.enabled = true;
    }

    public void OnClickNext(Button button)
    {
        button.interactable = false;
        barAnimator.SetTrigger("ExitLevel");
        Invoke(nameof(CreditsTransition), 1);
    }

    void CreditsTransition()
    {
        SceneManager.LoadScene("Credits");
    }
}
