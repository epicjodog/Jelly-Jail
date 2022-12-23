using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICredits : MonoBehaviour
{
    public Animator barTransition;
    public AudioManager audioMan;

    void Start()
    {
        audioMan = GetComponent<AudioManager>();
        audioMan.Play("CreditsBGM");
        StartCoroutine(WaitingForPost());
    }

    
    private IEnumerator WaitingForPost()
    {
        yield return new WaitForSeconds(37);
        barTransition.SetTrigger("ExitLevel");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("PostCredits");
    }
}
