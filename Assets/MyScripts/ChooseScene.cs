using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseScene : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject instruction;
    public GameObject credits;

   public void SelectScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ToInstruction()
    {
        mainScreen.SetActive(false);
        instruction.SetActive(true);
    }

    public void ToCredits()
    {
        mainScreen.SetActive(false);
        credits.SetActive(true);
    }

    public void InstructionsToMain()
    {
        mainScreen.SetActive(true);
        instruction.SetActive(false);
    }

    public void CreditsToMain()
    {
        mainScreen.SetActive(true);
        credits.SetActive(false);
    }

}
