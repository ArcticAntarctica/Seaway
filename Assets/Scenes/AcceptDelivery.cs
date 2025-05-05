using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AcceptDelivery : MonoBehaviour
{
    public static event Action OnDeliveryClicked;
    public static event Action OnFishingPortClicked;

    public void OnDeliveryClick()
    {
        OnDeliveryClicked?.Invoke();
    }

    public void OnFishingPortClick()
    {
        OnFishingPortClicked?.Invoke();
    }
}
