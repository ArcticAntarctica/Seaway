using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void PlayOnClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCommendationsClick()
    {
        this.gameObject.transform.GetChild(5).transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        StartCoroutine(FadeUICommendations(false));
    }

    public void OnCommendationsExitClick()
    {
        this.gameObject.transform.GetChild(5).transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        StartCoroutine(FadeUICommendations(true));
    }

    public void OnControlsClick()
    {
        this.gameObject.transform.GetChild(6).transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        StartCoroutine(FadeUIControls(false));
    }

    public void OnControlsExitClick()
    {
        this.gameObject.transform.GetChild(6).transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        StartCoroutine(FadeUIControls(true));
    }

    public void ExitOnClick()
    {
        Application.Quit();
    }

    private IEnumerator FadeUICommendations(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                this.gameObject.transform.GetChild(5).transform.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            //Ensure that the UI is fully off
            this.gameObject.transform.GetChild(5).transform.GetComponent<CanvasGroup>().alpha = 0;
            yield return null;
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                this.gameObject.transform.GetChild(5).transform.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            //Ensure that UI is fully on
            this.gameObject.transform.GetChild(5).transform.GetComponent<CanvasGroup>().alpha = 1;
            yield return null;
        }
    }

    private IEnumerator FadeUIControls(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                this.gameObject.transform.GetChild(6).transform.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            //Ensure that the UI is fully off
            this.gameObject.transform.GetChild(6).transform.GetComponent<CanvasGroup>().alpha = 0;
            yield return null;
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                this.gameObject.transform.GetChild(6).transform.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            //Ensure that UI is fully on
            this.gameObject.transform.GetChild(6).transform.GetComponent<CanvasGroup>().alpha = 1;
            yield return null;
        }
    }
}
