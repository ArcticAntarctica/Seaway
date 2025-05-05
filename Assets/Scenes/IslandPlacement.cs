using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPlacement : MonoBehaviour
{
    public List<GameObject> IslandList = new List<GameObject>();        //Saves all islands
    public List<int> RandomDeliveryPort = new List<int>();              //Saves delivery island numbers
    public List<int> RandomFishPort = new List<int>();                  //Saves fish port island numbers
    public List<Vector3> EventPlacementLocation = new List<Vector3>();  //Saves event locations
    public GameObject FishCluster, LootCluster;

    public static Vector3 DestroyedEvent;                               //Saves location of the destroyed event
    public static bool DeleteActive;                                    //Upon event destruction activates the if in update

    int DeliveryPortCount = 3;
    int FishPortCount = 2;
    int RandomEventCountMax = 8;
    public static int EventCountThisSession;                            //How many events are active in the session
    int Miss = 0; //To keep track if there was any overlapping throughout islands or not

    void Awake()
    {
        //Assigns delivery ports
        for (int i = 0; i < DeliveryPortCount; i++)
        {
            int RandomNum = Random.Range(0, IslandList.Count);
            while (RandomDeliveryPort.Contains(RandomNum) == true)
                RandomNum = Random.Range(0, IslandList.Count);
            RandomDeliveryPort[i] = RandomNum;
        }

        //Assigns fishing ports
        for (int i = 0; i < FishPortCount; i++)
        {
            int RandomNum = Random.Range(0, IslandList.Count);
            while (RandomFishPort.Contains(RandomNum) == true)
                RandomNum = Random.Range(0, IslandList.Count);
            RandomFishPort[i] = RandomNum;
        }

        for (int i = 0; i < IslandList.Count; i++)
        {
            //Gets coordinate for the island
            Vector3 RandomSpawn = new Vector3(Random.Range(-65, 65), -0.55f, Random.Range(-65, 65));

            //Makes sure there's more than one island placed (can't compare distances otherwise)
            if(i > 0)
            {
                //Runs through all already placed island locations
                bool StartCheck = true;
                while (StartCheck == true)
                {
                    Miss = 0;
                    for (int k = 0; k < i; k++)
                        //Checks if the distance between current spawn point and previously placed islands is okay
                        if (Vector3.Distance(RandomSpawn, IslandList[k].transform.position) <= 20 || Vector3.Distance(RandomSpawn, new Vector3(0, -0.55f, 0)) <= 20)
                        {
                            Miss++;
                            //Picks a new coordinate
                            RandomSpawn = new Vector3(Random.Range(-65, 65), -0.55f, Random.Range(-65, 65));
                        }
                    //If there was no overlapping, ends the loop. If there was, keeps on running the loop
                    //until there's absolutely no overlapping in any possible scenario
                    if (Miss == 0)
                        StartCheck = false;
                }
                //IslandPlacementLocation[i] = RandomSpawn;
            }
            //else IslandPlacementLocation[i] = RandomSpawn;

            //Assigns the coordinate to the island
            IslandList[i].transform.position = RandomSpawn;
            //Activates 3 deliveries
            if(RandomDeliveryPort.Contains(i) == true)
            {
                IslandList[i].transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
            }
            //Activate 2 fishing ports
            if(RandomFishPort.Contains(i) == true)
            {
                IslandList[i].transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
            }
        }



        EventCountThisSession = Random.Range(4, RandomEventCountMax - 1);
        Debug.Log("Event Sum: " + EventCountThisSession);

        for (int i = 0; i < EventCountThisSession; i++)
        {
            Vector3 RandomSpawn = new Vector3(Random.Range(-65, 65), 0f, Random.Range(-65, 65));

            if (i > 0)
            {
                //Runs through all already placed event + island locations
                bool StartCheck = true;
                while (StartCheck == true)
                {
                    Miss = 0;
                    for (int k = 0; k < IslandList.Count; k++)
                        //Checks if the distance between current spawn point and previously placed islands is okay
                        if (Vector3.Distance(RandomSpawn, IslandList[k].transform.position) <= 20 || Vector3.Distance(RandomSpawn, new Vector3(0, -0.49f, 0)) <= 20)
                        {
                            Miss++;
                            //Picks a new coordinate
                            RandomSpawn = new Vector3(Random.Range(-65, 65), 0f, Random.Range(-65, 65));
                        }
                    for(int j = 0; j < EventPlacementLocation.Count; j++)
                        //Checks if the distance between current spawn point and previously placed event is okay
                        if(Vector3.Distance(RandomSpawn, EventPlacementLocation[j]) <= 20)
                        {
                            Miss++;
                            //Picks a new coordinate
                            RandomSpawn = new Vector3(Random.Range(-65, 65), 0f, Random.Range(-65, 65));
                        }
                    //If there was no overlapping, ends the loop. If there was, keeps on running the loop
                    //until there's absolutely no overlapping in any possible scenario
                    if (Miss == 0)
                        StartCheck = false;
                }
                //Saves event location
                EventPlacementLocation[i] = RandomSpawn;
            }
            EventPlacementLocation[i] = RandomSpawn;

            //Spawns the event
            int RandomEventNum = Random.Range(0, 2);
            if(RandomEventNum == 0)
            Instantiate(FishCluster, RandomSpawn, Quaternion.identity);
            else Instantiate(LootCluster, RandomSpawn, Quaternion.identity);
        }

        //Spawns in new events every 2 minutes if EventCountThisSession isn't reaching RandomEventCountMax
        InvokeRepeating("SpawnEvent", 120f, 120f);
    }

    void SpawnEvent()
    {
        if(EventCountThisSession < RandomEventCountMax)
        {
            Vector3 RandomSpawn = new Vector3(Random.Range(-65, 65), 0f, Random.Range(-65, 65));

            //Runs through all already placed event + island locations
            bool StartCheck = true;
            while (StartCheck == true)
            {
                Miss = 0;
                for (int k = 0; k < IslandList.Count; k++)
                    //Checks if the distance between current spawn point and previously placed islands is okay
                    if (Vector3.Distance(RandomSpawn, IslandList[k].transform.position) <= 20 || Vector3.Distance(RandomSpawn, new Vector3(0, -0.49f, 0)) <= 20)
                    {
                        Miss++;
                        //Picks a new coordinate
                        RandomSpawn = new Vector3(Random.Range(-65, 65), 0f, Random.Range(-65, 65));
                    }
                for (int j = 0; j < EventPlacementLocation.Count; j++)
                    //Checks if the distance between current spawn point and previously placed event is okay
                    if (Vector3.Distance(RandomSpawn, EventPlacementLocation[j]) <= 30)
                    {
                        Miss++;
                        //Picks a new coordinate
                        RandomSpawn = new Vector3(Random.Range(-65, 65), 0f, Random.Range(-65, 65));
                    }
                //If there was no overlapping, ends the loop. If there was, keeps on running the loop
                //until there's absolutely no overlapping in any possible scenario
                if (Miss == 0)
                    StartCheck = false;
            }
            //Saves new event placement location in the first null spot on the list
            for(int i=0; i < EventPlacementLocation.Count; i++)
                if(EventPlacementLocation[i] == new Vector3(0,0,0))
                {
                    EventPlacementLocation[i] = RandomSpawn;
                    i = EventPlacementLocation.Count; //Ends cycle
                }

            //Spawns the event
            int RandomEventNum = Random.Range(0, 2);
            if (RandomEventNum == 0)
                Instantiate(FishCluster, RandomSpawn, Quaternion.identity);
            else Instantiate(LootCluster, RandomSpawn, Quaternion.identity);

            EventCountThisSession++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Sets deleted event's position to zero
        if(DeleteActive == true)
        {
            for (int i = 0; i < EventPlacementLocation.Count; i++)
                if (DestroyedEvent == EventPlacementLocation[i])
                    EventPlacementLocation[i] = new Vector3(0, 0, 0);
            DeleteActive = false;
        }
    }
}
