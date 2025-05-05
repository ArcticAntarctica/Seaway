using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class JsonSaveLoad : MonoBehaviour
{
    public Text MoneyText;
    Commendations CommendationData;

    private void Awake()
    {
        CommendationData = GetComponent<Commendations>(); //Attaches commendations script

        if (File.Exists(Application.dataPath + "/SeawayDataFile.json"))
            LoadFromJson();
        else SaveToJson();

        if(gameObject.scene.name == "SeawayMain")
        InvokeRepeating("SaveToJson", 60f, 60f);
    }

    private void OnEnable()
    {
        // Attaches observer
        PauseMenuButtons.SaveGame += SaveToJson;
    }

    private void OnDisable()
    {
        // Detaches observer
        PauseMenuButtons.SaveGame -= SaveToJson;
    }

    public void SaveToJson()
    {
        SaveLoadData data = new SaveLoadData();

        if (gameObject.scene.name == "SeawayMain")
            data.TotalOwnedMoney = int.Parse(MoneyText.text);
        data.TotalDeliverySum = CommendationData.DeliverySum;
        data.TotalMissionSum = CommendationData.MissionSum;
        data.TotalLootSum = CommendationData.LootSum;
        data.TotalFishSum = CommendationData.FishSum;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/SeawayDataFile.json", json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/SeawayDataFile.json");
        SaveLoadData data = JsonUtility.FromJson<SaveLoadData>(json);

        if (gameObject.scene.name == "SeawayMain")
            MoneyText.text = data.TotalOwnedMoney.ToString();
        CommendationData.DeliverySum = data.TotalDeliverySum;
        CommendationData.MissionSum = data.TotalMissionSum;
        CommendationData.LootSum = data.TotalLootSum;
        CommendationData.FishSum = data.TotalFishSum;
    }
}
