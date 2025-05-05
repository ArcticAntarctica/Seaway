using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FishingObserver : MonoBehaviour
{
    public Text FishCapacityText;                           //Text on the net
    public List<Image> FishImageList = new List<Image>();   //Fish images (up to 5)
    int FishNumInTheNet, MaxFishInTheNet = 10;              //How many fish the player has / Max fish limit
    public static bool IsFishNumInTheNetMax = false;        //For max fish check
    bool FishSellingRunning = false;                        //For coroutine so that the player doesn't spam click the seeling button
    int FishingMoneySum = 10;                               //Payout per 1 fish

    public static event Action FishMoneyPopUp;
    public static event Action LootFishMaxNotice;

    private void OnEnable()
    {
        // Attaches observer
        FishCapture.FishCaught += KickstartFishInTheNet;
        AcceptDelivery.OnFishingPortClicked += KickstartFishSelling;
        LootClaim.LootFish += KickstartLootFish;
    }

    private void OnDisable()
    {
        // Detaches observer
        FishCapture.FishCaught -= KickstartFishInTheNet;
        AcceptDelivery.OnFishingPortClicked -= KickstartFishSelling;
        LootClaim.LootFish -= KickstartLootFish;
    }

    void Start()
    {
        //Turns off all fish images
        for (int i = 0; i < FishImageList.Count; i++)
            FishImageList[i].gameObject.SetActive(false);
    }

    private void Update()
    {
        //If fish reaches max, locks away possibility to fish
        if (FishNumInTheNet == MaxFishInTheNet)
            IsFishNumInTheNetMax = true;
        else if (FishNumInTheNet < MaxFishInTheNet)
            IsFishNumInTheNetMax = false;
    }

    public void KickstartFishInTheNet()
    {
        if (FishNumInTheNet != MaxFishInTheNet)
        {
            //Adds and changes owned fish number
            FishNumInTheNet++;
            FishCapacityText.text = FishNumInTheNet + " / " + MaxFishInTheNet;

            //Activates fish images when the count gets uneven
            if (FishNumInTheNet % 2 == 1)
                for (int i = 0; i < FishImageList.Count; i++)
                    if (FishImageList[i].gameObject.activeSelf == false)
                    {
                        FishImageList[i].gameObject.SetActive(true);
                        i = FishImageList.Count; //Ends the cycle
                    }
        }
    }

    public void KickstartFishSelling()
    {
        //Checks if the player has any fish and if the selling is already in process
        if(FishNumInTheNet != 0 && FishSellingRunning == false)
        StartCoroutine(FishTakeAwayFromNet());
    }

    private IEnumerator FishTakeAwayFromNet()
    {
        FishSellingRunning = true;

        //Adds up money
        MoneyObserver.MoneyAdditionFactor = FishingMoneySum * FishNumInTheNet;
        FishMoneyPopUp?.Invoke();

        //Deactivates images and takes away the fish sum
        for (int i = FishNumInTheNet; i > 0; i--)
        {
            if (FishNumInTheNet % 2 == 1)
                for (int j = 0; j < FishImageList.Count; j++)
                    if (FishImageList[j].gameObject.activeSelf == true)
                    {
                        FishImageList[j].gameObject.SetActive(false);
                        j = FishImageList.Count; //Ends the cycle
                    }

            FishNumInTheNet--;
            FishCapacityText.text = FishNumInTheNet + " / " + MaxFishInTheNet;

            yield return new WaitForSeconds(0.3f);
        }

        FishSellingRunning = false;
    }


    int LootFishAmountMax = 5;

    public void KickstartLootFish()
    {
        int RandomFishNum = UnityEngine.Random.Range(2, LootFishAmountMax + 1);

        if (IsFishNumInTheNetMax == false)
        {
            //Saves the sum before adding loot
            int TemporaryOldNum = FishNumInTheNet;
            FishNumInTheNet += RandomFishNum;

            //If sum is over the max, sets it spot on to max
            if (FishNumInTheNet > MaxFishInTheNet)
                FishNumInTheNet = MaxFishInTheNet;

            FishCapacityText.text = FishNumInTheNet + " / " + MaxFishInTheNet;

            //Goes from old number to new addition result and activates the images
            for (int i = TemporaryOldNum + 1; i <= FishNumInTheNet; i++)
            {
                if (i % 2 == 1)
                    for (int j = 0; j < FishImageList.Count; j++)
                        if (FishImageList[j].gameObject.activeSelf == false)
                        {
                            FishImageList[j].gameObject.SetActive(true);
                            j = FishImageList.Count; //Ends the cycle
                            Debug.Log("Fish Added At: " + i);
                        }
            }
        }
        //Calls notice
        else if (IsFishNumInTheNetMax == true)
            LootFishMaxNotice?.Invoke();
    }
}