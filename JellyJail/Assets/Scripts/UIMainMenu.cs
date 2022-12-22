using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [Header("Page Anims")]
    public Animator helpAnim;
    public Animator settingsAnim;
    public Animator creditsAnim;
    public Animator itemsAnim;
    // Origin: x = 2260, y = 498
    // New Position: x = 1516, 498
    [Header("Credits Page")]
    public GameObject[] creditsPanels;
    public Button nextButton;
    public Button previousButton;
    int currentPage = 0;
    [Header("Items Page")]
    public GameObject[] itemPanels;
    public Button nextIButton;
    public Button previousIButton;
    int currentItemPage = 0;

    private Animator currentPanel;
    AudioManager audioMan;

    void Start()
    {
        previousButton.interactable = false;
        previousIButton.interactable = false;

        audioMan = GetComponent<AudioManager>();
        audioMan.Play("Intro");
        Invoke(nameof(TransitionToMainMenuMusic), 85);
    }

    void Update()
    {
        
    }

    void TransitionToMainMenuMusic()
    {
        audioMan.VolumeFadeOut("Intro", true);
        audioMan.Play("Main Menu");
    }

    public void OnClickPlay()
    {
        print("Playing Game");
        // SceneManager.LoadScene("Level_1");
    }

    public void OnClickSettings()
    {
        settingsAnim.SetTrigger("settingsOpen");
        currentPanel = settingsAnim;
    }

    public void OnClickHelp()
    {
        helpAnim.SetTrigger("helpOpen");
        currentPanel = helpAnim;
    }

    public void OnClickCredits()
    {
        creditsAnim.SetTrigger("creditsOpen");
        currentPanel = creditsAnim;
    }

    public void OnClickItems()
    {
        itemsAnim.SetTrigger("itemsOpen");
        currentPanel = itemsAnim;
    }

    public void OnClickBack(string temp)
    {
        currentPanel.SetTrigger(temp);
    }

    /////////////////////////////////////////////////////////////////////////////////
    // Credits page switching
    public void CreditSwapNext()
    {
        creditsPanels[currentPage].SetActive(false);

        if(currentPage != creditsPanels.Length - 1)
        {
            currentPage++;
            print(currentPage);
            if(currentPage == creditsPanels.Length - 1)
            {
                nextButton.interactable = false;
                // currentPage = creditsPanels.Length;
            }

            if(currentPage > 0)
            {
                previousButton.interactable = true;
            }
        }
        else

        print(currentPage);

        creditsPanels[currentPage].SetActive(true);
    }
    public void CreditSwapPrevious()
    {
        creditsPanels[currentPage].SetActive(false);

        if(currentPage != 0)
        {
            currentPage--;
            if(currentPage == 0)
            {
                previousButton.interactable = false;
                currentPage = 0;
            }

            if(currentPage < creditsPanels.Length)
            {
                nextButton.interactable = true;
            }
        }
        
        creditsPanels[currentPage].SetActive(true);
    }
    public void CreditResetPages()
    {
        creditsPanels[currentPage].SetActive(false);
        creditsPanels[0].SetActive(true);
    }

    /////////////////////////////////////////////////////////////////////////////////
    // Items Page Switching
    public void ItemSwapNext()
    {
        itemPanels[currentItemPage].SetActive(false);

        if(currentItemPage != itemPanels.Length - 1)
        {
            currentItemPage++;
            if(currentItemPage == itemPanels.Length - 1)
            {
                nextIButton.interactable = false;
            }

            if(currentItemPage > 0)
            {
                previousIButton.interactable = true;
            }
        }
        else

        print(currentItemPage);

        itemPanels[currentItemPage].SetActive(true);
    }
    public void ItemSwapPrevious()
    {
        itemPanels[currentItemPage].SetActive(false);

        if(currentItemPage != 0)
        {
            currentItemPage--;
            if(currentItemPage == 0)
            {
                previousIButton.interactable = false;
                currentItemPage = 0;
            }

            if(currentItemPage < itemPanels.Length)
            {
                nextIButton.interactable = true;
            }
        }
        
        itemPanels[currentItemPage].SetActive(true);
    }
    public void ItemResetPages()
    {
        itemPanels[currentItemPage].SetActive(false);
        itemPanels[0].SetActive(true);
    }

    public void OnClickQuit()
    {
        print("Exiting Application...");
        Application.Quit();
    }
}
