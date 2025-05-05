using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuButtons : MonoBehaviour
{
    public static event Action SaveGame;

    public void ResumeOnClick()
    {
        this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(8).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnCommendationsClick()
    {
        this.gameObject.transform.GetChild(8).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(7).gameObject.SetActive(true);
    }

    public void ControlsOnClick()
    {
        this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(8).gameObject.SetActive(true);
    }

    public void MenuOnClick()
    {
        Time.timeScale = 1f;
        SaveGame?.Invoke();
        SceneManager.LoadScene(0);
    }

    public void ExitOnClick()
    {
        SaveGame?.Invoke();
        Application.Quit();
    }
}
