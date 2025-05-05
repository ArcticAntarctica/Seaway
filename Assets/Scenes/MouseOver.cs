using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    //Makes sure that the player's mouse is on an island when submitting cargo
    private void OnMouseEnter()
    {
        //Debug.Log("Over");
        DeliveryObserver.MouseOverIsland = gameObject.name;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Exit");
        DeliveryObserver.MouseOverIsland = null;
    }
}
