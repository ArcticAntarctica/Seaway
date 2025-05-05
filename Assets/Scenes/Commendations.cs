using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Commendations : MonoBehaviour
{
    public int DeliverySum, MissionSum, LootSum, FishSum;
    public List<Image> CellsA = new List<Image>(); //Deliveries
    public List<Image> CellsB = new List<Image>(); //Missions
    public List<Image> CellsC = new List<Image>(); //Loot
    public List<Image> CellsD = new List<Image>(); //Fish

    public List<int> CellsAMax = new List<int>(); //Deliveries score
    public List<int> CellsBMax = new List<int>(); //Missions score
    public List<int> CellsCMax = new List<int>(); //Loot score
    public List<int> CellsDMax = new List<int>(); //Fish score

    int CellsCount = 5; //5 cells per one cell block

    private void OnEnable()
    {
        // Attaches observer
        DeliveryObserver.DeliveryCommendation += KickstartDeliveryAddition;
        DeliveryObserver.MissionCommendation += KickstartMissionAddition;
        LootClaim.LootCommendation += KickstartLootAddition;
        FishCapture.FishCommendation += KickstartFishAddition;
    }

    private void OnDisable()
    {
        // Detaches observer
        DeliveryObserver.DeliveryCommendation -= KickstartDeliveryAddition;
        DeliveryObserver.MissionCommendation -= KickstartMissionAddition;
        LootClaim.LootCommendation -= KickstartLootAddition;
        FishCapture.FishCommendation -= KickstartFishAddition;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Upon start assigns all saved commendation scores from the save file
        for(int i = 0; i < CellsCount; i++)
        {
            CellsA[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = DeliverySum + " / " + CellsAMax[i];
            CellsB[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = MissionSum + " / " + CellsBMax[i];
            CellsC[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = LootSum + " / " + CellsCMax[i];
            CellsD[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = FishSum + " / " + CellsDMax[i];

            //If scores are higher or equal to designated scores of each cell, activates a mask that shows the player that they've completed that commendation
            if (DeliverySum >= CellsAMax[i])
                CellsA[i].transform.GetChild(3).gameObject.SetActive(true);

            if (MissionSum >= CellsBMax[i])
                CellsB[i].transform.GetChild(3).gameObject.SetActive(true);

            if (LootSum >= CellsCMax[i])
                CellsC[i].transform.GetChild(3).gameObject.SetActive(true);

            if (FishSum >= CellsDMax[i])
                CellsD[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    //Adds to the sums and checks if any new commendations are reached (turns on the mask)
    public void KickstartDeliveryAddition()
    {
        DeliverySum++;
        for(int i = 0; i < CellsCount; i++)
        {
            CellsA[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = DeliverySum + " / " + CellsAMax[i];

            if (DeliverySum >= CellsAMax[i] && CellsA[i].transform.GetChild(3).gameObject.activeSelf == false)
                CellsA[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void KickstartMissionAddition()
    {
        MissionSum++;
        for (int i = 0; i < CellsCount; i++)
        {
            CellsB[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = MissionSum + " / " + CellsBMax[i];

            if (MissionSum >= CellsBMax[i] && CellsB[i].transform.GetChild(3).gameObject.activeSelf == false)
                CellsB[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void KickstartLootAddition()
    {
        LootSum++;
        for (int i = 0; i < CellsCount; i++)
        {
            CellsC[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = LootSum + " / " + CellsCMax[i];

            if (LootSum >= CellsCMax[i] && CellsC[i].transform.GetChild(3).gameObject.activeSelf == false)
                CellsC[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void KickstartFishAddition()
    {
        FishSum++;
        for (int i = 0; i < CellsCount; i++)
        {
            CellsD[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = FishSum + " / " + CellsDMax[i];

            if (FishSum >= CellsDMax[i] && CellsD[i].transform.GetChild(3).gameObject.activeSelf == false)
                CellsD[i].transform.GetChild(3).gameObject.SetActive(true);
        }
    }
}
