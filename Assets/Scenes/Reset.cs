using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DeliveryObserver.InventorySlotsMax = false;
        FishingObserver.IsFishNumInTheNetMax = false;
        IslandPlacement.EventCountThisSession = 0;
        IslandPlacement.DeleteActive = false;
        LootClaim.CanDeleteLoot = false;
    }
}
