using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FishCapture : MonoBehaviour
{
    public Text FishNum, Key1, Key2, Key3;
    public Image TimeCounter, FishStartToggle, FishKeys;
    public Image Success1, Success2, Success3;
    public Sprite Tick, Cross;
    public GameObject Player;
    public Canvas FishSquadCanvas;
    int FishMax = 6;
    bool IsFishingActive = false;
    bool IsPlayerInProximity = false;

    public static event Action FishCaught;
    public static event Action FishMaxNotice;
    public static event Action FishCommendation;

    void Start()
    {
        Player = GameObject.Find("ShipModel");
        FishNum.text = UnityEngine.Random.Range(1, FishMax + 1).ToString();
    }

    void Update()
    {
        //Checks distance between player and this fish squad and turns the UI on accordingly
        if (Vector3.Distance(transform.position, Player.transform.position) < 5)
        {
            //Starts UI fade in
            if (IsPlayerInProximity == false)
            {
                //In case of player moving in and out of the area, stops the previous coroutine
                StopCoroutine(FadeUI(true));
                StartCoroutine(FadeUI(false));
                IsPlayerInProximity = true;
            }

            if (Input.GetKeyDown(KeyCode.F) && IsFishingActive == false && (int.Parse(FishNum.text) != 0))
            {
                //Checks if there's still space in fishing inventory
                if(FishingObserver.IsFishNumInTheNetMax == false)
                {
                    StartCoroutine(Fishing());
                    FishStartToggle.GetComponent<CanvasGroup>().alpha = 0; //Toggle Main off
                    FishKeys.GetComponent<CanvasGroup>().alpha = 1; //Toggle Fishing on
                    IsFishingActive = true;
                }
                //If not, issues a notice
                else FishMaxNotice?.Invoke();
            }
        }
        else
        {
            //Starts UI fade out
            if (IsPlayerInProximity == true)
            {
                //In case of player moving in and out of the area, stops the previous coroutine
                StopCoroutine(FadeUI(false));
                StartCoroutine(FadeUI(true));
                IsPlayerInProximity = false;
            }
        }

        //Destroys entire fish squad if there are no more fishes left
        if (int.Parse(FishNum.text) == 0)
        {
            IslandPlacement.DeleteActive = true;
            IslandPlacement.DestroyedEvent = this.gameObject.transform.position;
            IslandPlacement.EventCountThisSession--;
            Destroy(gameObject, 1);
        }
    }

    private IEnumerator Fishing()
    {
        //Resets the count so that the player presses keys in order
        int KeyCount = 1;

        //Assigns letters
        char randomLetter;
        do
        {
            randomLetter = (char)('A' + UnityEngine.Random.Range(0, 26));
        } while (randomLetter == 'I' || randomLetter == 'M' || randomLetter == 'W' || randomLetter == 'A' || randomLetter == 'S' || randomLetter == 'D');
        Key1.text = randomLetter.ToString();

        do
        {
            randomLetter = (char)('A' + UnityEngine.Random.Range(0, 26));
        } while (randomLetter == 'I' || randomLetter == 'M' || randomLetter == 'W' || randomLetter == 'A' || randomLetter == 'S' || randomLetter == 'D');
        Key2.text = randomLetter.ToString();

        do
        {
            randomLetter = (char)('A' + UnityEngine.Random.Range(0, 26));
        } while (randomLetter == 'I' || randomLetter == 'M' || randomLetter == 'W' || randomLetter == 'A' || randomLetter == 'S' || randomLetter == 'D');
        Key3.text = randomLetter.ToString();


        //Timer begins
        float duration = 3f; // 3 seconds
        float normalizedTime = 1;
        while (normalizedTime >= 0f)
        {
            TimeCounter.fillAmount = normalizedTime;
            normalizedTime -= Time.deltaTime / duration;
            yield return null;

            //Key1 Sequence
            if (KeyCount == 1 && Input.anyKeyDown)
            {
                string pressedKey = Input.inputString;

                if (pressedKey.ToUpper() == Key1.text)
                {
                    Key1.text = " ";
                    Success1.gameObject.SetActive(true); //Activates tick
                    Success1.sprite = Tick;
                    KeyCount++;
                    yield return null;
                }
                else
                {
                    Key1.text = " ";
                    Success1.gameObject.SetActive(true); //Activates cross
                    Success1.sprite = Cross;
                    yield return new WaitForSeconds(1f);

                    FishingFinish();
                    yield break;
                }
            }

            //Key2 Sequence
            if (KeyCount == 2 && Input.anyKeyDown)
            {
                string pressedKey = Input.inputString;

                if (pressedKey.ToUpper() == Key2.text)
                {
                    Key2.text = " ";
                    Success2.gameObject.SetActive(true);
                    Success2.sprite = Tick;
                    KeyCount++;
                    yield return null;
                }
                else
                {
                    Key2.text = " ";
                    Success2.gameObject.SetActive(true);
                    Success2.sprite = Cross;
                    yield return new WaitForSeconds(1f);

                    FishingFinish();
                    yield break;
                }
            }

            //Key3 Sequence
            if (KeyCount == 3 && Input.anyKeyDown)
            {
                string pressedKey = Input.inputString;

                if (pressedKey.ToUpper() == Key3.text)
                {
                    Key3.text = " ";
                    Success3.gameObject.SetActive(true);
                    Success3.sprite = Tick;

                    FishCaught?.Invoke(); //Adds fish images to the net
                    FishCommendation?.Invoke(); //Adds +1 to fish commendation

                    yield return new WaitForSeconds(1f);

                    FishingFinish();
                    yield break;
                }
                else
                {
                    Key3.text = " ";
                    Success3.gameObject.SetActive(true);
                    Success3.sprite = Cross;
                    yield return new WaitForSeconds(1f);

                    FishingFinish();
                    yield break;
                }
            }
        }

        //When timer runs out
        Key1.text = " ";
        Key2.text = " ";
        Key3.text = " ";
        Success1.gameObject.SetActive(true);
        Success2.gameObject.SetActive(true);
        Success3.gameObject.SetActive(true);
        Success1.sprite = Cross;
        Success2.sprite = Cross;
        Success3.sprite = Cross;
        yield return new WaitForSeconds(1f);

        FishingFinish();
        yield return null;
    }

    private void FishingFinish()
    {
        FishNum.text = (int.Parse(FishNum.text) - 1).ToString();
        IsFishingActive = false;

        //Reset the toggle view
        FishStartToggle.GetComponent<CanvasGroup>().alpha = 1; //Toggle Main on
        FishKeys.GetComponent<CanvasGroup>().alpha = 0; //Toggle Fishing off

        //Empty out the fields
        Key1.text = " ";
        Key2.text = " ";
        Key3.text = " ";

        //Deactivate ticks and crosses
        Success1.gameObject.SetActive(false);
        Success2.gameObject.SetActive(false);
        Success3.gameObject.SetActive(false);
    }

    private IEnumerator FadeUI(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
            {
                FishSquadCanvas.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            //Ensure that the UI is fully off
            FishSquadCanvas.GetComponent<CanvasGroup>().alpha = 0;
            yield return null;
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * 2)
            {
                FishSquadCanvas.GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
            //Ensure that UI is fully on
            FishSquadCanvas.GetComponent<CanvasGroup>().alpha = 1;
            yield return null;
        }
    }
}